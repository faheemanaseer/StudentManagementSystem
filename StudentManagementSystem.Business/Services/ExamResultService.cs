using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Services
{
    public class ExamResultService : IExamResultService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ExamResultService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context; 
            _mapper = mapper;
        }

        public async Task AddExamResultAsync(ExamResultdto dto)
        {
            bool isEnrolled = await _context.StudentCourses.AnyAsync(sc => sc.StudentId == dto.StudentId && sc.CourseId == dto.CourseId);


            if (!isEnrolled)
            {
                throw new Exception("Student is not Enrolled in selected Course");
            }

            string grade;
            if (dto.Marks >= 85) grade = "A";
            else if (dto.Marks >= 70) grade = "B";
            else if (dto.Marks > 50) grade = "C";
            else grade = "F";

            var examResult = new ExamResult
            {
                StudentId = dto.StudentId,
                CourseId = dto.CourseId,
                Marks = dto.Marks,
                Grade = grade
            };

            _context.ExamResult.Add(examResult);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ExamResultdto>> GetResultByCoursesAsync(int courseId)
        {
            var results  =  await _context.ExamResult
                    .Include(er => er.Student)
                    .Include(er => er.Course)
                    .Where(er => er.CourseId == courseId)
                    .ToListAsync();

            return _mapper.Map<List<ExamResultdto>>(results);
        }

        public async Task<List<ExamResultdto>> GetResultsByStudentsAsync(int studentId)
            {
                var results = await _context.ExamResult
                .Include(er => er.Course)
                .Include(er => er.Student)
                .Where(er=>er.StudentId == studentId)
                .ToListAsync();

            return _mapper.Map<List<ExamResultdto>>(results);
        }

        public async Task<IEnumerable<ExamResultdto>> GetAllResultsAsync()
        {
            return await _context.ExamResult
                .Select(e => new ExamResultdto
                {
                    StudentId = e.StudentId,
                    CourseId = e.CourseId,
                    Marks = e.Marks,
                    StudentName = e.Student.Name, 
                    CourseName = e.Course.Title,  
                    Grade = CalculateGrade(e.Marks) 
                }).ToListAsync();
        }

        public async Task<IEnumerable<ExamResultdto>> GetResultsByStudentsNameAsync(string query, int? CourseId)
        {
            var results = await _context.ExamResult
                    .Include(er => er.Student)
                    .Include(er => er.Course)
                    .Where(er => er.Student.Name.Contains(query))
                    
                    .ToListAsync();
            return _mapper.Map<IEnumerable<ExamResultdto>>(results);
        }

        static private string CalculateGrade(double Marks)
        {
            string grade;
            if (Marks >= 85) grade = "A";
            else if (Marks >= 70) grade = "B";
            else if (Marks > 50) grade = "C";
            else grade = "F";
            return grade;

        }
    }
     
}
  