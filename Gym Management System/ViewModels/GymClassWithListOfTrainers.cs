using Gym_Management_System.Models;

namespace Gym_Management_System.ViewModels
{
    public class GymClassWithListOfTrainers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Schedule { get; set; }
        public int TrainerId { get; set; }

        public List<Trainer> Trainers { get; set; } = new List<Trainer>();
    }
}
