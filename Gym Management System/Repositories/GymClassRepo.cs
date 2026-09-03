using Gym_Management_System.Data;
using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.Repositories
{
    public class GymClassRepo : IGymClassRepo
    {
        private GymDbContext _context;
        public GymClassRepo(GymDbContext context)
        {
            _context = context;
        }
        public void AddGymClass(GymClass gymClass)
        {
            _context.GymClasses.Add(gymClass);
            _context.SaveChanges();
        }

        public void DeleteGymClass(int id)
        {
            var gymClass = _context.GymClasses.Find(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
                _context.SaveChanges();
            }
        }
        

        public List<GymClass> GetAllGymClasses()
        {
            return _context.GymClasses.Include(gc => gc.Trainer).ToList();
        }
        public List<GymClass> GetGymClassesByTrainer(int trainerId)
        {
            return _context.GymClasses.Include(gc => gc.Trainer).Where(gc => gc.TrainerId == trainerId).ToList();
        }
        public List<GymClass> GetGymClassesByMember(int memberId)
        {
            return _context.GymClasses
                .Where(gc => gc.Enrollments.Any(e => e.MemberId == memberId))
                .ToList();
        }

        public GymClass GetGymClass(int id)
        {
            return _context.GymClasses.Include(gc => gc.Trainer).FirstOrDefault(gc => gc.Id == id);
        }

        public void UpdateGymClass(GymClass gymClass)
        {
            _context.GymClasses.Update(gymClass);
            _context.SaveChanges();
        }
    }
}
