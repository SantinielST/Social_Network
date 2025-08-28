using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Services;
using SocialNetwork.DLL.Entities;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers
{
    public class AccountManagerController : Controller
    {
        private readonly UserService _userService;
        private readonly SignInManager<UserEntity> _signInManager;

        public AccountManagerController(UserService userService, SignInManager<UserEntity> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        // GET /Account/Login
        [HttpGet("Login")] //убрала лишний get, объединила в один.
                           //Если returnUrl не передавать, пользователя всегда будет возвращать на главную страницу (Home/Index).
        public IActionResult Login(string returnUrl = null)
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model); 
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(model);
            }

            // Получаем пользователя через сервис
            var userEntity = await _userService.GetByEmailAsync(model.Email);
            if (userEntity == null)
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                return View(model);
            }

            // Логинимся через SignInManager
            var result = await _signInManager.PasswordSignInAsync(userEntity.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Profile", "User", new { id = userEntity.Id });
            }

            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            return View(model);
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

