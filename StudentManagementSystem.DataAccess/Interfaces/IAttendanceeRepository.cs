using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.DataAccess.Interfaces
{
    public interface IAttendanceeRepository
    {
        Task<List<Attendancee>> GetByCourseAndDateAsync(int courseId, DateTime date);
        Task AddRangeAsync(List<Attendancee> attendances);
        Task AddAsync(Attendancee attendance);
        
        Task UpdateAsync(Attendancee attendance);
        Task<Attendancee> GetByCourseAndDateAndStudentAsync(int courseId, DateTime date, int studentId);
        Task DeleteByCourseAndDateAsync(int courseId, DateTime date);
    }

}
