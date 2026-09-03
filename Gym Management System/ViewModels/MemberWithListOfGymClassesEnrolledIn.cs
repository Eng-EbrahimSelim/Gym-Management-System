using Gym_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Gym_Management_System.ViewModels
{
    public class MemberWithListOfGymClassesEnrolledIn
    {
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        public List<GymClass> GymClasses { get; set; } = new List<GymClass>();
    }
}
