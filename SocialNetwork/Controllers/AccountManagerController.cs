using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.Extentions;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers;

public class AccountManagerController(IMapper mapper, UserService userService, FriendService friendService) : Controller
{
    private readonly IMapper _mapper = mapper;

    private readonly UserService _userService = userService;
    private readonly FriendService _friendService = friendService;

    [Route("Login")]
    [HttpGet]
    public IActionResult Login()
    {
        return View("Home/Login");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }

    [Authorize]
    [Route("MyPage")]
    [HttpGet]
    public async Task<IActionResult> MyPage()
    {
        var user = User;

        var result = await _userService.GetUserAsync(user);

        var model = new UserViewModel(result);

        model.Friends = _friendService.GetFriendsByUser(model.User);

        return View("MyPage", model);
    }

    [Route("Edit")]
    [HttpGet]
    public IActionResult Edit()
    {
        var user = User;

        var result = _userService.GetUserAsync(user);

        var editmodel = _mapper.Map<UserEditViewModel>(result.Result);

        return View("EditUserProfile", editmodel);
    }

    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User().Convert(model);

            var result = await _userService.UpdateAsync(user, model.UserId);

            if (result.Succeeded)
            {
                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Ошибка, обновление не удалось");
                return RedirectToAction("Edit", "AccountManager");
            }
        }
        else
        {
            ModelState.AddModelError("", "Некорректные данные");
            return View("EditUserProfile", model);
        }
    }

    [Route("Login")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _userService.CheckPasswordAsync(model.Email, model.Password);

            if (result)
            {
                await _userService.SignInAsync(model.Email, false);
                return RedirectToAction("MyPage", "AccountManager");
            }
            else
            {
                ModelState.AddModelError("", "Неправильный логин и (или) пароль");
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _userService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Route("UserList")]
    [HttpGet]
    public async Task<IActionResult> UserList(string search)
    {
        var model = await CreateSearch(search);
        return View("UserList", model);
    }

    [Route("AddFriend")]
    [HttpPost]
    public async Task<IActionResult> AddFriend(string id)
    {
        _friendService.AddFriend(User, id);

        return RedirectToAction("MyPage", "AccountManager");
    }

    [Route("DeleteFriend")]
    [HttpPost]
    public async Task<IActionResult> DeleteFriend(string id)
    {
        _friendService.DeleteFriend(User, id);

        return RedirectToAction("MyPage", "AccountManager");
    }

    private async Task<SearchViewModel> CreateSearch(string search)
    {
        var currentuser = User;

        var result = await _userService.GetUserAsync(currentuser);

        var list = _userService.GetUsersForSearch(search, result.Id);

        var withfriend = _friendService.GetFriendsByUser(result);

        var data = new List<UserWithFriendExt>();

        list.ForEach(x =>
        {
            var t = _mapper.Map<UserWithFriendExt>(x);
            t.IsFriendWithCurrent = withfriend.Where(y => y.Id == x.Id || x.Id == result.Id).Count() != 0;
            data.Add(t);
        });

        var model = new SearchViewModel()
        {
            UserList = data
        };

        return model;
    }
}