using Gym_Management_System.Interfaces;
using Gym_Management_System.Models;
using Gym_Management_System.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private IMemberRepo _Repo;
        private IGymClassRepo _ClassRepo;
        public MemberController(IMemberRepo repo, IGymClassRepo classRepo)
        {
            _Repo = repo;
            _ClassRepo = classRepo;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var list = _Repo.GetAllMembers();
            return View(list);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {
            //list of gym classes that the member is enrolled in
            var member = _Repo.GetMemberById(id);
            MemberWithListOfGymClassesEnrolledIn vm = new MemberWithListOfGymClassesEnrolledIn
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                GymClasses = _ClassRepo.GetGymClassesByMember(id)
            };
            return View(vm);
        }
        [Authorize]
        public IActionResult MembersByGymClass(int id)
        {
            var members = _Repo.GetMembersByGymClassId(id);
            ViewBag.ClassId = id;
            return View(members);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            _Repo.AddMember(member);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var member = _Repo.GetMemberById(id);
            return View(member);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(Member member)
        {
            if (!ModelState.IsValid)
            {
                return View(member);
            }
            _Repo.UpdateMember(member);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _Repo.DeleteMember(id);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EnrollInClass()
        {
           ViewBag.GymClasses = _ClassRepo.GetAllGymClasses().Select(gc => new SelectListItem
            {
                Value = gc.Id.ToString(),
                Text = gc.Name
            });
            ViewBag.Members = _Repo.GetAllMembers().Select(m => new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Name
            });

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EnrollInClass(Enrollment enrollment)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GymClasses = _ClassRepo.GetAllGymClasses().Select(gc => new SelectListItem
                {
                    Value = gc.Id.ToString(),
                    Text = gc.Name
                });
                ViewBag.Members = _Repo.GetAllMembers().Select(m => new SelectListItem
                {
                    Value = m.Id.ToString(),
                    Text = m.Name
                });
                return View(enrollment);
            }

            _Repo.EnrollMemberInClass(enrollment);
            return RedirectToAction("Index");
        }

    }
}
