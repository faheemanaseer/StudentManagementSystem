using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Entities.Entity;
//using AutoMapper.Extensions.Microsoft.DependencyInjection;

namespace StudentManagementSystem.Business.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<StudentDto, Student>()
            .ForMember(dest => dest.UId, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.Ignore())
            .ReverseMap();

<<<<<<< HEAD
=======
            CreateMap<Course, CourseDto>()
    .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.Name));

            CreateMap<CourseDto, Course>();
            CreateMap<Instructor, InstructorDto>().ReverseMap();

>>>>>>> 2de589ff8d367a21056bc7bf70232ff5f1c705e0
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudentCourse,StudentCourse>().ReverseMap();

            CreateMap<ExamResult, ExamResultdto>()
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Title))
                .ReverseMap();


        }
                
    }
}
