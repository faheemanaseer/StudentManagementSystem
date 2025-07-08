namespace StudentManagementSystem.Web.Models
{
    public class AdminStudentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public List<string> AssignedCourses { get; set; }
    }
}
