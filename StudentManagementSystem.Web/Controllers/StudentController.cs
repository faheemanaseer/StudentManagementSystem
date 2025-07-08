using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Web.Models;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace StudentManagementSystem.Web.Controllers
{

    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Profile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();
            var dto = await _studentService.GetProfileAsync(userId);

            if (dto == null)
                return RedirectToAction("CreateProfile");

            return View(dto);
        }
        [HttpGet]
        public IActionResult CreateProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var model = new StudentViewModel
            {
                Email = email // this will be shown in the view
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProfile(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();
                TempData["Error"] = string.Join(", ", errors);
                return View(model);
            }

         
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(email))
            {
                TempData["Error"] = "User email is not available. Cannot create profile.";
                return View(model);
            }

            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(email) || !int.TryParse(userIdStr, out var userId))
            {
                TempData["Error"] = "User information is missing.";
                return View(model);
            }
            var dto = new StudentDto
            {
                Name = model.Name,
                Email = email,         
                Phone = model.Phone,
                Age = model.Age
            };

            await _studentService.CreateProfileAsync(dto, userId);
            return RedirectToAction("Profile");
        }


        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();
            var dto = await _studentService.GetProfileAsync(userId);

            if (dto == null)
                return RedirectToAction("CreateProfile");

            var viewModel = new StudentViewModel
            {
                UId = dto.UId,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Age = dto.Age
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(StudentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
               .Select(x => $"{x.Key}: {string.Join(", ", x.Value.Errors.Select(e => e.ErrorMessage))}")
               .ToList();

                TempData["Error"] = string.Join(" | ", errors); 
            }

            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();

            var dto = new StudentDto
            {
                UId = model.UId,
                Name = model.Name,
                //Email = email,
                Phone = model.Phone,
                Age = model.Age
            };

            await _studentService.UpdateProfileAsync(dto, userId);
            return RedirectToAction("Profile");
        }

        public async Task<IActionResult> EnrolledCourses()
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out var userId))
                return Unauthorized();
            var courses = await _studentService.GetEnrolledCoursesAsync(userId);
            return View(courses);
        }
    }
}