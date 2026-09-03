using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private ITrainerRepo _Repo;
        private IGymClassRepo _GymClassRepo;
        public TrainerController(ITrainerRepo repo, IGymClassRepo gymClassRepo)
        {
            _Repo = repo;
            _GymClassRepo = gymClassRepo;
        }
        public IActionResult Index()
        {
            var list = _Repo.GetAllTrainers();
            return View(list);
        }
        public IActionResult Details(int id)
        {
            ViewBag.GymClasses = _GymClassRepo.GetGymClassesByTrainer(id);
            var trainer = _Repo.GetTrainerById(id);
            return View(trainer);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View(trainer);
            }

            _Repo.AddTrainer(trainer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var trainer = _Repo.GetTrainerById(id);
            return View(trainer);
        }
        [HttpPost]
        public IActionResult Edit(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View(trainer);
            }
            _Repo.UpdateTrainer(trainer);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
           _Repo.DeleteTrainer(id);
            return RedirectToAction("Index");
        }

    }
}
