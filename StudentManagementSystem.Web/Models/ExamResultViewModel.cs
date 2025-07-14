using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Web.Models
{
    public class ExamResultViewModel
    {
        [Required]
        public int? StudentId { get; set; }

        [Required]
        public int? CourseId { get; set; }

        [Required]
        [Range(0, 100)]
        public int? Marks { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem>? Students { get; set; }

        [BindNever]
        public IEnumerable<SelectListItem>? Courses { get; set; }


        [BindNever]
        public IEnumerable<ExamResultDisplayViewModel>? Results { get; set; }
    }

}
