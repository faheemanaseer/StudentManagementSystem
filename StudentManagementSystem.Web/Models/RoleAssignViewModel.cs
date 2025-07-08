using StudentManagementSystem.Entities.Entity;
namespace StudentManagementSystem.Web.Models

{
    public class RoleAssignViewModel
    {
        public List<User> Users { get; set; }   
        public List<Role> Roles { get; set; }
    }
}
