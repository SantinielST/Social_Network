using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DLL.Entities;
using SocialNetwork.ViewModels;


namespace SocialNetwork.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IMapper _mapper; //преобразует ViewModel -> доменная модель User
        private readonly SignInManager<UserEntity> _signInManager;
        private readonly UserService _userService;
        // UserManager у нас в сервисах

        public RegisterController(UserService userService, SignInManager<UserEntity> signInManager, IMapper mapper)
        {
            _userService = userService;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("Home/Register");
        }

        [Route("RegisterPart2")]
        [HttpGet]
        public IActionResult RegisterPart2(RegisterViewModel model)
        {
            return View("RegisterPart2", model);
        }


        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View("RegisterPart2", model);

            var user = _mapper.Map<User>(model);
            var result = await _userService.CreateAsync(user, model.PasswordReg);

            if (result.Succeeded)
            {
                var entity = _mapper.Map<UserEntity>(user); // чтобы SignIn знал, кого логинить
                await _signInManager.SignInAsync(entity, false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);

            return View("RegisterPart2", model);
        }
    }
}