using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers;

public class RegisterController : Controller
{
    private readonly IMapper _mapper; //преобразует ViewModel -> доменная модель User

    private readonly UserService _userService;

    public RegisterController(IMapper mapper, UserService userService)
    {
        _mapper = mapper;
        _userService = userService;
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
            var user = _mapper.Map<User>(model);

            var result = await _userService.CreateUserAsync(user, model.PasswordReg);
            
            if (result.Succeeded)
            {
                await _userService.SignInAsync(user.Email, false);
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