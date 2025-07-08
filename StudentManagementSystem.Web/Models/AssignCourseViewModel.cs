using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Web.Models
{
    public class AssignCourseViewModel
    {
        [Required]
        public int StudentId { get; set; }
        [BindNever]
        public string StudentName { get; set; }
        [Required]
        public int CourseId { get; set; }

        //public List<SelectListItem> Students { get; set; }
        [BindNever]
        public List<SelectListItem> Courses { get; set; }
    }

}
