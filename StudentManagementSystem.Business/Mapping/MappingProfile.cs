using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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

            CreateMap<Course, CourseDto>()
    .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.Name));

            CreateMap<CourseDto, Course>();
            CreateMap<Instructor, InstructorDto>().ReverseMap();

            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<StudentCourse,StudentCourse>().ReverseMap();
        }
                
    }
}
