using Gym_Management_System.Data;
using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;

namespace Gym_Management_System.Repositories
{
    public class TrainerRepo : ITrainerRepo
    {
        private GymDbContext _context;
        public TrainerRepo(GymDbContext context)
        {
            _context = context;
        }
        public void AddTrainer(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            _context.SaveChanges();    
        }

        public void DeleteTrainer(int id)
        {
            var trainer = _context.Trainers.Find(id);
            if (trainer != null)
            {
                _context.Trainers.Remove(trainer);
                _context.SaveChanges();
            }
        }
        

        public List<Trainer> GetAllTrainers()
        {
            return _context.Trainers.ToList();
        }

        public Trainer GetTrainerById(int id)
        {
            return _context.Trainers.FirstOrDefault(t => t.Id == id);
        }
        

        public void UpdateTrainer(Trainer trainer)
        {
            _context.Trainers.Update(trainer);
            _context.SaveChanges();
        }
    }
}
