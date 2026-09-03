using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;
using Gym_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    public class GymClassController : Controller
    {
        private IGymClassRepo _Repo;
        private ITrainerRepo _TrainerRepo;
        public GymClassController(IGymClassRepo repo, ITrainerRepo trainerRepo)
        {
            _Repo = repo;
            _TrainerRepo = trainerRepo;
        }
        public IActionResult Index()
        {
            var list = _Repo.GetAllGymClasses();
            ViewBag.Trainers = _TrainerRepo.GetAllTrainers().Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();
            return View(list);

        }
        public IActionResult GetClassesByTrainer(int? trainerId)
        {
            var gymClasses = _Repo.GetAllGymClasses();

            if (trainerId.HasValue)
            {
                gymClasses = gymClasses
                    .Where(g => g.TrainerId == trainerId.Value)
                    .ToList();
            }

            return PartialView("_GymClasses", gymClasses);
        }
        public IActionResult Details(int id)
        {
            //we need to include trainer to show name --ok

            var gymClass = _Repo.GetGymClass(id);
           

            return View(gymClass);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewBag.Trainers = _TrainerRepo.GetAllTrainers().Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(GymClass gymClass)
        {
            //we need a vm to show the list of trainsers

            if (!ModelState.IsValid)
            {
                ViewBag.Trainers = _TrainerRepo.GetAllTrainers().Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                });
                return View(gymClass);
            }

            _Repo.AddGymClass(gymClass);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var gymClass = _Repo.GetGymClass(id);
            GymClassWithListOfTrainers vm = new GymClassWithListOfTrainers
            {
                Id = gymClass.Id,
                Name = gymClass.Name,
                Description = gymClass.Description,
                Schedule = gymClass.Schedule,
                TrainerId = gymClass.TrainerId,
                Trainers = _TrainerRepo.GetAllTrainers()
            };
            //we need a vm to show the list of trainsers
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(GymClassWithListOfTrainers gymClassVm)
        {
            //same here, we need a vm to show the list of trainers
            if (!ModelState.IsValid)
            {
                gymClassVm.Trainers = _TrainerRepo.GetAllTrainers();
                return View(gymClassVm);
            }
            GymClass gymClass = new GymClass
            {
               Id = gymClassVm.Id,
                Name = gymClassVm.Name,
                Description = gymClassVm.Description,
                Schedule = gymClassVm.Schedule,
                TrainerId = gymClassVm.TrainerId
            };
            _Repo.UpdateGymClass(gymClass);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _Repo.DeleteGymClass(id);
            return RedirectToAction("Index");
        }
    }
}
