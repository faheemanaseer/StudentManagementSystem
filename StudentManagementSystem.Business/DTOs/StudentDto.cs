using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.DTOs
{
    public class StudentDto
    {
        public int UId { get; set; }
        public string Name { get; set; }
        
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public string? CardPath { get; set; }
        
    }
}
