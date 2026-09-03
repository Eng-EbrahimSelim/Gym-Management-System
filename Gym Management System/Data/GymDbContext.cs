using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.Data
{
    public class GymDbContext: DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Trainer> Trainers { get; set; }
        public DbSet<Models.GymClass> GymClasses { get; set; }
        public DbSet<Models.Member> Members { get; set; }
        public DbSet<Models.Enrollment> Enrollments { get; set; }
    
    }
}
