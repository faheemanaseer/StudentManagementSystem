using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Business.Services;
using StudentManagementSystem.DataAccess.Interfaces;
using StudentManagementSystem.Web.Models;


namespace StudentManagementSystem.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IStudentCourseService _studentCourseService;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentCourseRepository _studentCourseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentService _studentService;
        public AdminController(ICourseService courseService, IStudentCourseService studentCourseService, IStudentRepository studentRepository, IStudentCourseRepository studentCourseRepository, ICourseRepository courseRepository, IStudentService studentService)
        {
            _courseService = courseService;
            _studentCourseService = studentCourseService;
            _studentRepository = studentRepository;
            _studentCourseRepository = studentCourseRepository;
            _courseRepository = courseRepository;
            _studentService = studentService;
        }

        public async Task<IActionResult> Courses()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> EditCourse(int id)
        {
            var dto = await _courseService.GetByIdAsync(id);
            return View(new CourseViewModel { SId = dto.SId, Title = dto.Title });
        }

        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _courseService.UpdateAsync(new CourseDto { SId = model.SId, Title = model.Title });
            return RedirectToAction("Courses");
        }
        [HttpGet]
        public async Task<IActionResult> AddCourse()
        {
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = courses;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _courseService.AddAsync(new CourseDto { Title = model.Title });
            return RedirectToAction("Courses");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction("Courses");
        }
        public async Task<IActionResult> Students()
        {
            var students = await _studentCourseService.GetAllStudentsAsync();
            return View(students); 
        }

        public async Task<IActionResult> AssignCourse(int studentId)
        {
            var students = await _studentCourseService.GetAllStudentsAsync();
            var courses = await _studentCourseService.GetAllCoursesAsync();
            var assignedCourses = await _studentCourseService.GetAssignedCoursesAsync(studentId);

            ViewBag.StudentId = studentId;
            ViewBag.StudentList = students.Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.UId.ToString()
            }).ToList();

            ViewBag.CourseList = courses.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.SId.ToString()
            }).ToList();

            ViewBag.AssignedCourses = assignedCourses;

            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> AssignCourse(int StudentId, int courseId)
        {
            if (!await _studentCourseService.IsAlreadyEnrolledAsync(StudentId, courseId))
            {
                await _studentCourseService.AssignCourseAsync(StudentId, courseId);
                TempData["Success"] = "Course assigned successfully.";
            }
            else
            {
                TempData["Error"] = "This student is already enrolled in the selected course.";
            }

            return RedirectToAction("AssignCourse", new { StudentId });
        }
        [HttpPost]
        public async Task<IActionResult> AddCourseAjax(CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest("Validation failed: " + string.Join(", ", errors));
            }

            await _courseService.AddAsync(new CourseDto { Title = model.Title });

            var updatedCourses = await _courseService.GetAllAsync();
            ViewBag.IsAjax = true;
            return PartialView("Courses", updatedCourses);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            var student = await _studentService.SearchByNameAsync(name);
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> SearchStudent(string name)
        {
            var student = await _studentService.SearchByNameAsync(name);
            return PartialView("_Students", student);
        }
    }
}
