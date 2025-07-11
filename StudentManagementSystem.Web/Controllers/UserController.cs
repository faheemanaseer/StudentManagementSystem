using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Entities.Entity;
using StudentManagementSystem.Web.Models;
using System.Security.Claims;
using System.Threading.Tasks;


namespace StudentManagementSystem.Web.Controllers
{
  
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var existingUser = await _userService.GetByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError(string.Empty, "User already exists.");
                return View(model);
            }

            var newUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = model.Password,
                RoleId = 4
            };

            await _userService.RegisterAsync(newUser);

            HttpContext.Session.SetInt32("UserId", newUser.Id); 
            return RedirectToAction("Profile", "Student");
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Console.WriteLine("MVC Login Hit");
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userService.LoginAsync(model.Email, model.Password);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View(model);
            }
            if (user.RoleId == 4 || user.Role?.Name == "PendingApproval")
            {
                ModelState.AddModelError("", "Your account is pending approval.");
                return RedirectToAction("PendingApproval");
            }



            var roleName = await _userService.GetRoleNameByIdAsync(user.RoleId);
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Your role is invalid or not found.");
                return View(model);
            }


            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.Name),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.Role, roleName ?? "")
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            if (user.Role.Name == "Admin")
                return RedirectToAction("Students", "Admin");
            else if (user.Role.Name == "Student")
                return RedirectToAction("EnrolledCourses", "Student");
            else if (user.Role.Name == "SuperAdmin")
                return RedirectToAction("Index", "RoleManagement");

            return RedirectToAction("Login");
        
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MyCookieAuth");
            return RedirectToAction("Login", "User");
        }
        [AllowAnonymous]
        public IActionResult PendingApproval()
        {
            ViewBag.Message = TempData["Message"] ?? "Your account is awaiting approval.";
            return View();
        }

    }
}
