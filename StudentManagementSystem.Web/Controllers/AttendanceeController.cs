using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Business.Services;
using StudentManagementSystem.Web.Models;

namespace StudentManagementSystem.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AttendanceeController : Controller
    {
        private readonly IAttendanceeService _attendanceeService;
        public AttendanceeController(IAttendanceeService attendanceeService)
        {
            _attendanceeService = attendanceeService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MarkAttendance(int courseId, DateTime? date)
        {
            if (!date.HasValue)
            {
                date = DateTime.Today;
            }

            var students = await _attendanceeService.GetAttendanceAsync(courseId, date.Value);

            ViewBag.CourseId = courseId;
            ViewBag.Date = date.Value;

            return View(students);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAttendance(List<AttendanceeDto> attendanceList, DateTime date)
        {
            if (attendanceList == null || !attendanceList.Any())
            {
                TempData["Error"] = "No attendance data received.";
                return RedirectToAction("Courses", "Admin");
            }

            foreach (var a in attendanceList)
                a.Date = date;

            int courseId = attendanceList[0].CourseId;


            await _attendanceeService.DeleteExistingRecordsAsync(courseId, date);


            await _attendanceeService.SaveAttendanceAsync(attendanceList);
            TempData["Success"] = "Attendance saved successfully.";

            return RedirectToAction("MarkAttendance", new { courseId, date });
        }

        



    }
}
