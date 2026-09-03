using Gym_Management_System.Models;

namespace Gym_Management_System.Interfaces
{
    public interface ITrainerRepo
    {
        public List<Trainer> GetAllTrainers();
        public Trainer GetTrainerById(int id);
        public void AddTrainer(Trainer trainer);
       
        public void UpdateTrainer(Trainer trainer);
        public void DeleteTrainer(int id);
    }
}
