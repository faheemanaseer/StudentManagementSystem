using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.DataAccess.Interfaces;
using StudentManagementSystem.DataAccess.Repositories;
using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IStudentRepository _studentRepo;
        private readonly IStudentCourseRepository _studentCourseRepo;
        private readonly IMapper _mapper;
        public CourseService(ICourseRepository courseRepo, IStudentRepository studentRepo, IStudentCourseRepository studentCourseRepo,IMapper mapper)
        {
            _courseRepo = courseRepo;
            _studentRepo = studentRepo;
            _studentCourseRepo = studentCourseRepo;
            _mapper = mapper;
        }

        public async Task<List<CourseDto>> GetAllAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return _mapper.Map<List<CourseDto>>(courses);
        }

        public async Task<CourseDto> GetByIdAsync(int id)
        {
            var c = await _courseRepo.GetByIdAsync(id);
            return _mapper.Map<CourseDto>(c);
        }

        public async Task AddAsync(CourseDto dto)
        {
            var course = _mapper.Map<Course>(dto);
            await _courseRepo.AddAsync(course);
        }

        public async Task UpdateAsync(CourseDto dto)
        {
            var course = await _courseRepo.GetByIdAsync(dto.SId);
            if (course == null) return;

            _mapper.Map(dto, course);

            await _courseRepo.UpdateAsync(course);
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepo.GetByIdAsync(id);
            await _courseRepo.DeleteAsync(course);
        }
        public async Task<List<SelectListItem>> GetAllStudentsAsync()
        {
            var students = await _studentRepo.GetAllAsync();
            return students.Select(s => new SelectListItem
            {
                Value = s.UId.ToString(),
                Text = s.Name
            }).ToList();
        }

        public async Task<List<SelectListItem>> GetAllCoursesAsync()
        {
            var courses = await _courseRepo.GetAllAsync();
            return courses.Select(c => new SelectListItem
            {
                Value = c.SId.ToString(),
                Text = c.Title
            }).ToList();
        }

        public async Task AssignCourseAsync(int studentId, int courseId)
        {
            var assignment = new StudentCourse
            {
                StudentId = studentId,
                CourseId = courseId
            };
            await _studentCourseRepo.AddAsync(assignment);
        }

    }

}
