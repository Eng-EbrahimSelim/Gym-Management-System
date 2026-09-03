using Gym_Management_System.Models;

namespace Gym_Management_System.Interfaces
{
    public interface IGymClassRepo
    {
        public List<GymClass> GetAllGymClasses();
        public GymClass GetGymClass(int id);
        public List<GymClass> GetGymClassesByTrainer(int trainerId);
        public List<GymClass> GetGymClassesByMember(int memberId);
        public void AddGymClass(GymClass gymClass);
        public void UpdateGymClass(GymClass gymClass);
        public void DeleteGymClass(int id);
    }
}
