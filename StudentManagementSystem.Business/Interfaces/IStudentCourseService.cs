﻿using Microsoft.AspNetCore.Mvc.Rendering;
using StudentManagementSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface IStudentCourseService
    {
        Task AssignCourseAsync(int studentId, int courseId);
        Task<bool> IsAlreadyEnrolledAsync(int studentId, int courseId);

        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<List<StudentDto>> GetAllStudentsAsync();

        Task<List<CourseDto>> GetAssignedCoursesAsync(int studentId);
    }
}


