using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Web.Models;
namespace StudentManagementSystem.Web.Models
{
    public class CoursePageViewModel
    {
        public CourseViewModel Course { get; set; } = new();
        public List<CourseDto> CourseList { get; set; } = new();
    }
}
