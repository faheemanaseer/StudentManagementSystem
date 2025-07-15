using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Entities.Entity;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAllAsync();
        Task<CourseDto> GetByIdAsync(int id);
        Task AddAsync(CourseDto dto);
        Task UpdateAsync(CourseDto dto);
        Task<List<CourseInstructorDto>> GetCoursesWithInstructorsAsync();

        Task<List<SelectListItem>> GetAllStudentsAsync();
        Task<List<SelectListItem>> GetAllCoursesAsync();
        Task AssignCourseAsync(int studentId, int courseId);
        Task DeleteAsync(int id);
        Task AssignInstructorAsync(int courseId, int instructorId);
        Task<List<Instructor>> GetAllInstructorsAsync();

    }

}
