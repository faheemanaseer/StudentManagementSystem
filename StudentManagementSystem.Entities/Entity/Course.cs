using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Entities.Entity
{
    public class Course
    {
        [Key]
        public int SId { get; set; }
        [Required]
        public string Title { get; set; }
        [ValidateNever]
        public ICollection<StudentCourse> StudentCourses { get; set; }
    }
}
