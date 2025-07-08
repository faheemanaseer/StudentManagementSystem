using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;

using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Entities.Entity;

namespace StudentManagementSystem.Business.Services
{
    public class StudentCourseService : IStudentCourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentCourseService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AssignCourseAsync(int studentId, int courseId)
        {
            var studentExists = await _context.Students.AnyAsync(s => s.UId == studentId);
            var courseExists = await _context.Courses.AnyAsync(c => c.SId == courseId);

            if (!studentExists || !courseExists)
                throw new Exception("Student or Course does not exist.");

            var alreadyEnrolled = await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);

            if (!alreadyEnrolled)
            {
                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                };

                _context.StudentCourses.Add(studentCourse);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId)
        {
            return await _context.StudentCourses
                .AnyAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
        }

        public async Task<List<CourseDto>> GetAllCoursesAsync()
        {
            var course= await _context.Courses.ToListAsync();
            return _mapper.Map<List<CourseDto>>(course);

        }

        public async Task<List<StudentDto>> GetAllStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
                return _mapper.Map<List<StudentDto>>(students);
        }

        public async Task<List<CourseDto>> GetAssignedCoursesAsync(int studentId)
        {
            var courses= await _context.StudentCourses
                .Where(sc => sc.StudentId == studentId)
                .Include(sc => sc.Course)
                .Select(sc => sc.Course)
                .ToListAsync();
            return _mapper.Map<List<CourseDto>>(courses);
           
                
        }
    }
}
