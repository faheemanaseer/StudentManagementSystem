using StudentManagementSystem.Entities.Entity;

namespace StudentManagementSystem.DataAccess.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetByUserIdAsync(int userId);
        Task CreateAsync(Student student);
        Task UpdateAsync(Student student);
        Task SaveAsync();
        Task<List<Student>> SearchByNameAsync(string name);

        Task<List<Student>> GetStudentsByCourseId(int courseId);

        Task<List<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<List<Course>> GetEnrolledCoursesAsync(int userId);
        

    }


}
