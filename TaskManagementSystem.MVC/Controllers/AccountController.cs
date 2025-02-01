using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Application.DTOs;
using TaskManagementSystem.Application.Interfaces;
using TaskManagementSystem.Core.Helpers;

namespace TaskManagementSystem.MVC.Controllers
{
    public class AccountController(IAuthService authService) : Controller
    {
        private readonly IAuthService authService = authService;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
                return View(new ResponseBase(false, model, string.Join("\n", ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage)).Select(e => e).ToList())));

            var res = await authService.LogInAsync(model.Email, model.Password);

            if (res.Success)
                return RedirectToAction("Index", "Tasks");

            else return View(res);
        }

        public async Task<IActionResult> Logout()
        {
            await authService.LogOutAsync();

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return View(new ResponseBase(false, model, string.Join("\n", ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage)).Select(e => e).ToList())));

            if (!model.Email.Trim().Equals(model.EmailConfirmation.Trim(), StringComparison.CurrentCultureIgnoreCase))
            {
                return View(new ResponseBase(false, model, "Email Confirmation must be same as email"));
            }

            var res = await authService.RegisterAsync(model.Email, model.Password);

            if (res.Success)
                return RedirectToAction("SignIn", "Account");

            else return View(res);
        }
    }
}
