using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.Business.Services;

namespace StudentManagementSystem.Web.Controllers
{
    [Authorize(Roles="Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        public InstructorController(IInstructorService instructorService, ICourseService courseService,IMapper mapper)
        {
            _instructorService = instructorService;
            _courseService = courseService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();
            return View(instructors);
        }

        public IActionResult Add() => View();

        [HttpPost]
        public async Task<IActionResult> Add(InstructorDto dto)
        {
            if (ModelState.IsValid)
            {
                await _instructorService.AddAsync(dto);
                TempData["Success"] = "Instructor added successfully!";
                return RedirectToAction("Index");
            }

            return View(dto);
        }
        [HttpGet]
        public async Task<IActionResult> AssignInstructor(int courseId)
        {
            var instructors = await _courseService.GetAllInstructorsAsync();
            var dtoList = _mapper.Map<List<InstructorDto>>(instructors); 

            ViewBag.CourseId = courseId;
            return View(dtoList); 
        }



        [HttpPost]
        public async Task<IActionResult> AssignInstructor(AssignInstructorDto model)
        {
            await _courseService.AssignInstructorAsync(model.CourseId, model.InstructorId);
            TempData["Success"] = "Instructor assigned successfully!";
            return RedirectToAction("CourseInstructor","Instructor");
        }
        [HttpGet]
        public async Task<IActionResult> CourseInstructor()
        {
            var data = await _courseService.GetCoursesWithInstructorsAsync();
            return View(data);
        }


    }

}
