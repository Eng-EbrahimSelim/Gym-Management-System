using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management_System.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int GymClassId { get; set; }

        public DateTime EnrollmentDate { get; set; }

        [ForeignKey("MemberId")]
        public Member? Member { get; set; }
        [ForeignKey("GymClassId")]
        public GymClass? GymClass { get; set; }
    }
}
