using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.DTOs
{
    public class ExamResultdto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public double Marks { get; set; }
        public string Grade { get; set; }
    }

}
