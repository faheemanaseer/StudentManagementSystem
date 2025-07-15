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
<<<<<<< HEAD
        
=======
        // public IdentityUser IdentityUser { get; set; }  
>>>>>>> 2de589ff8d367a21056bc7bf70232ff5f1c705e0
        public int? UserId { get; set; } 
        public User User { get; set; }

    }
}
