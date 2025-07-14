using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.DataAccess.Interfaces;
using StudentManagementSystem.Entities.Entity;
using StudentManagementSystem.Web.Models;
using System.Security.Claims;

namespace StudentManagementSystem.Web.Controllers
{
    public class ExamResultController : Controller
    {

        private readonly IExamResultService _examResultService;
        private readonly ApplicationDbContext _context;
        
        private  readonly IStudentRepository _studentrepository;
        private readonly ICourseRepository _courserepository;

        public ExamResultController(IExamResultService examResultService,ApplicationDbContext context , DataAccess.Interfaces.IStudentRepository studentRepository , DataAccess.Interfaces.ICourseRepository courseRepository)
        {
           
            _examResultService = examResultService;
            _context = context;
            _studentrepository = studentRepository;
            _courserepository = courseRepository;

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Add(int? studentId = null)
        {
            var model = new ExamResultViewModel
            {
                Students = new SelectList(await _context.Students.ToListAsync(), "UId", "Name"),
                Courses = new SelectList(await _context.Courses.ToListAsync(), "SId", "Title"),
            };


            var selectedStudentId = studentId ?? await _context.Students.Select(s => s.UId).FirstOrDefaultAsync();
            

            if (selectedStudentId != 0)
            {
                var results = await _examResultService.GetResultsByStudentsAsync(selectedStudentId);
                model.Results = results.Select(dto => new ExamResultDisplayViewModel
                {
                    StudentName = dto.StudentName,
                    CourseName = dto.CourseName,
                    Marks = (int)dto.Marks,
                    Grade = dto.Grade
                }).ToList();

                model.StudentId = selectedStudentId;
            }

            return View(model);
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(ExamResultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(ModelState);
                }

                await PopulateStudentsAndCoursesAsync(model);
                return View(model);
            }

            if (!model.StudentId.HasValue || !model.CourseId.HasValue || !model.Marks.HasValue)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest("Please select a student, course and enter marks.");
                }
                ModelState.AddModelError("", "Please select a student, course and enter marks.");
                await PopulateStudentsAndCoursesAsync(model);
                return View(model);
            }

            var dto = new ExamResultdto
            {
                StudentId = model.StudentId.Value,
                CourseId = model.CourseId.Value,
                Marks = model.Marks.Value
            };

            try
            {
                var already_assigned = await _context.ExamResult.AnyAsync(e => e.StudentId == model.StudentId && e.CourseId == model.CourseId);

                if (already_assigned)
                {
                    return BadRequest($"The student has already been assigned in selected course");
                }
                
                await _examResultService.AddExamResultAsync(dto);

                var updatedResults = (await _examResultService.GetResultsByStudentsAsync(model.StudentId.Value))
                    .Select(dto => new ExamResultDisplayViewModel
                    {
                        StudentName = dto.StudentName,
                        CourseName = dto.CourseName,
                        Marks = (int)dto.Marks,
                        Grade = dto.Grade
                    });

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_ExamResultsTable", updatedResults);
                }

                await PopulateStudentsAndCoursesAsync(model);
                return View(model);
            }
            catch (Exception ex)
            {
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return BadRequest(ex.Message);
                }

                ModelState.AddModelError("", ex.Message);
                await PopulateStudentsAndCoursesAsync(model);
                return View(model);
            }
        }




        //[Authorize(Roles = "Admin")]
        //[HttpGet]
        //public async Task<IActionResult> List(int? courseId, string sortOrder)
        //{
        //    ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "SId", "Title");

        //    var results = courseId.HasValue
        //        ? await _examResultService.GetResultByCoursesAsync(courseId.Value)
        //        : await _examResultService.GetResultByCoursesAsync(0);

        //    if (!string.IsNullOrEmpty(sortOrder))
        //    {
        //        results = sortOrder switch
        //        {
        //            "asc" => results.OrderBy(r => r.Marks).ToList(),
        //            "desc" => results.OrderByDescending(r => r.Marks).ToList(),
        //            _ => results
        //        };
        //    }

        //    ViewBag.SelectedCourse = courseId;
        //    ViewBag.SortOrder = sortOrder;

        //    return View(results);
        //}

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> List(int? courseId, string sortOrder)
        {
            ViewBag.Courses = new SelectList(await _context.Courses.ToListAsync(), "SId", "Title");

            // Fetch all results if courseId is null, otherwise filter by courseId
            var results = !courseId.HasValue
                ? await _examResultService.GetAllResultsAsync() // Method to get all results
                : await _examResultService.GetResultByCoursesAsync(courseId.Value);

            // Transform ExamResultdto to ExamResultDisplayViewModel
            var displayResults = results.Select(dto => new ExamResultDisplayViewModel
            {
                StudentName = dto.StudentName,
                CourseName = dto.CourseName,
                Marks = (int)dto.Marks,
                Grade = dto.Grade
            }).ToList();

            if (!string.IsNullOrEmpty(sortOrder))
            {
                displayResults = sortOrder switch
                {
                    "asc" => displayResults.OrderBy(r => r.Marks).ToList(),
                    "desc" => displayResults.OrderByDescending(r => r.Marks).ToList(),
                    _ => displayResults
                };
            }

            ViewBag.SelectedCourse = courseId;
            ViewBag.SortOrder = sortOrder;

            return View(displayResults);
        }


        private async Task PopulateStudentsAndCoursesAsync(ExamResultViewModel model)
        {
            model.Students = new SelectList(await _context.Students.ToListAsync(), "UId", "Name");
            model.Courses = new SelectList(await _context.Courses.ToListAsync(), "SId", "Title");
        }



        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> MyResults()
        {
            //var userEmail = User.Identity.Name;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            //var userEmail = await _context.Users.Include(s => s.Email).FirstOrDefaultAsync(s => s.User.Name == userr);


            var student = await _context.Students
                .Include(s => s.StudentCourses)
                .FirstOrDefaultAsync(s => s.User.Email == userEmail);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var enrolledCourses = await _context.Courses
                .Where(c => c.StudentCourses.Any(sc => sc.StudentId == student.UId))
                .ToListAsync();

            if (!enrolledCourses.Any())
            {
                ViewBag.Message = "You are not assigned in any course yet. So no marks to view.";
                return View(new List<ExamResultDisplayViewModel>());
            }

            var results = await _examResultService.GetResultsByStudentsAsync(student.UId);

            var displayResults = enrolledCourses.Select(course =>
            {
                var result = results.FirstOrDefault(r => r.CourseId == course.SId);

                return new ExamResultDisplayViewModel
                {
                    CourseName = course.Title,
                    Marks = result?.Marks != null ? (int)result.Marks : 0,
                    Grade = result?.Grade ?? "Not assigned yet"
                };
            }).ToList();

            return View(displayResults);
        }

    }
}
