using StudentManagementSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface IExamResultService
    {
        Task AddExamResultAsync(ExamResultdto dto);
        Task<List<ExamResultdto>> GetResultByCoursesAsync(int courseId);
        Task<List<ExamResultdto>> GetResultsByStudentsAsync(int studentId);
        Task<IEnumerable<ExamResultdto>> GetAllResultsAsync();
    }
}
