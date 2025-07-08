using StudentManagementSystem.Business.DTOs;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface IStudentService
    {
        Task<StudentDto> GetProfileAsync(int userId);
        Task CreateProfileAsync(StudentDto dto, int userId);
        Task UpdateProfileAsync(StudentDto dto, int userId);
        Task<List<CourseDto>> GetEnrolledCoursesAsync(int userId);

    }

}
