using System.ComponentModel.DataAnnotations;

namespace Gym_Management_System.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Specialization { get; set; }

        public bool IsAdmin { get; set; }

        public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}
