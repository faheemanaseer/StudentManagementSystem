﻿using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem.Entities.Entity
{
    public class StudentCourse
    {
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
