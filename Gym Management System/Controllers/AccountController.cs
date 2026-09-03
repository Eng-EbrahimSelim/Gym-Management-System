using Gym_Management_System.Interfaces;
using Gym_Management_System.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gym_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly ITrainerRepo _trainerRepo;

        public AccountController(ITrainerRepo trainerRepo)
        {
            _trainerRepo = trainerRepo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var trainer = _trainerRepo.GetAllTrainers()
                .FirstOrDefault(t =>
                    t.Email == model.Email &&
                    t.Password == model.Password);

            if (trainer == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password"
                );

                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, trainer.Id.ToString()),
                new Claim(ClaimTypes.Name, trainer.Name),
                new Claim(ClaimTypes.Email, trainer.Email)
            };

            if (trainer.IsAdmin)
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, "Admin")
                );
            }
            else
            {
                claims.Add(
                    new Claim(ClaimTypes.Role, "Trainer")
                );
            }

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );
            if (trainer.IsAdmin)
            {
                return RedirectToAction("Index", "Trainer");
            }

            return RedirectToAction("Index", "GymClass");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}