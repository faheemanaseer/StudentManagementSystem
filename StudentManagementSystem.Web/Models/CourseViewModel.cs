using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Web.Models
{
    public class CourseViewModel
    {
        public int SId { get; set; }

        [Required]
        public string Title { get; set; }



    }
}
