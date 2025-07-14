using AutoMapper;
using StudentManagementSystem.Business.DTOs;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.DataAccess.Interfaces;
using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IInstructorRepository _repo;
        private readonly IMapper _mapper;

        public InstructorService(IInstructorRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<InstructorDto>> GetAllAsync()
        {
            var instructors = await _repo.GetAllAsync();
            return _mapper.Map<List<InstructorDto>>(instructors);
        }

        public async Task AddAsync(InstructorDto dto)
        {
            var entity = _mapper.Map<Instructor>(dto);
            await _repo.AddAsync(entity);
        }
    }

}
