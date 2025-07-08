using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(int id);
        Task AddAsync(CourseDto dto);
        Task UpdateAsync(CourseDto dto);
        Task<List<SelectListItem>> GetAllStudentsAsync();
        Task<List<SelectListItem>> GetAllCoursesAsync();
        Task AssignCourseAsync(int studentId, int courseId);
        Task DeleteAsync(int id);
    }

}
