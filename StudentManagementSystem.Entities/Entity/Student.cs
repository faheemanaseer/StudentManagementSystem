using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Entities.Entity
{
    public class Student
    {
        [Key]
        public int UId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required, Range(1, 120, ErrorMessage = "Enter valid age")]
        public int Age { get; set; }
        [ValidateNever]
        public ICollection<StudentCourse> StudentCourses { get; set; }
        // public IdentityUser IdentityUser { get; set; }  
        public int? UserId { get; set; } 
        public User User { get; set; }

    }
}
