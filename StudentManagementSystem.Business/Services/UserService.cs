using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Business.Interfaces;
using StudentManagementSystem.DataAccess.Data;
using StudentManagementSystem.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string?> GetRoleNameByIdAsync(int roleId)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            return role?.Name;
        }

        public async Task RegisterAsync(User user)
        {
            var existing = await _context.Users.AnyAsync(u => u.Email == user.Email);
            if (existing)
                throw new Exception("User already exists.");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

    }
}
