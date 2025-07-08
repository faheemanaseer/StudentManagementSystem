using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagementSystem.Entities.Entity;

namespace StudentManagementSystem.Business.Interfaces
{
    public interface IUserService
    {
        Task RegisterAsync(User user);

        Task<User> LoginAsync(string email, string password);
        Task<string?> GetRoleNameByIdAsync(int roleId);

        Task<User> GetByEmailAsync(string email);
    }
}
