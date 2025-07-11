using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Entities.Entity;
using StudentManagementSystem.Web.Models;

namespace StudentManagementSystem.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoleManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList(); 
            var roles = _context.Roles
                .Select(r => new Role { Id = r.Id, Name = r.Name })
                .ToList();

            return View(new RoleAssignViewModel
            {
                Users = users,
                Roles = roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int userId, int roleId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                
                

                user.RoleId = roleId;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Role assigned successfully.";
            }
            if (user.RoleId != 4)
            {
                TempData["Error"] = "This user already has a role assigned.";
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

    }

}
