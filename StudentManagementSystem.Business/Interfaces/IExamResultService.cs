using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface IExamResultService
    {
        Task AddExamResultAsync(ExamResultdto dto);
        Task<List<ExamResultdto>> GetResultByCoursesAsync(int courseId);
        Task<List<ExamResultdto>> GetResultsByStudentsAsync(int studentId);
        Task<IEnumerable<ExamResultdto>> GetAllResultsAsync();
        Task<IEnumerable<ExamResultdto>>  GetResultsByStudentsNameAsync(string query, int? CourseId);
    }
}
