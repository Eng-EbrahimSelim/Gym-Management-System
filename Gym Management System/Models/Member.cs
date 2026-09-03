using System.ComponentModel.DataAnnotations;

namespace Gym_Management_System.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<Enrollment>? Enrollments { get; set; } = new List<Enrollment>();
    }
}
