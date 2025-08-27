using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.ViewModels;


namespace SocialNetwork.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IMapper _mapper; //преобразует ViewModel -> доменная модель User

        private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;

        public RegisterController(IMapper mapper, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Register(RegisterViewModel model) //возврат на вторую форму
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<UserEntity>(model);

                var result = await _userManager.CreateAsync(user, model.PasswordReg);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("RegisterPart2", model);
        }
    }
}