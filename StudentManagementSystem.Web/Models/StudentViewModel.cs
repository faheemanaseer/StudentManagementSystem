using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Web.Models
{
    public class StudentViewModel
    {
        public int UId { get; set; }

        [Required]
        public string Name { get; set; }

        
      
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required, Range(1, 120)]
        public int Age { get; set; }
    }

}
