using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.BLL.Models;
using SocialNetwork.BLL.Services;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.UoW;
using SocialNetwork.Extentions;
using SocialNetwork.ViewModels;

namespace SocialNetwork.Controllers;


public class AccountManagerController(
    IMapper mapper,
    UserService userService,
    FriendService friendService,
    UserManager<UserEntity> userManager,
    IUnitOfWork unitOfWork)
    : Controller
{
    [HttpGet]
    [Route("Login")]
    public IActionResult Login(string returnUrl = null) 
    {
        if (User.Identity is { IsAuthenticated: true })
        {
            // если пользователь уже авторизован, редирект на Home/Index
            return RedirectToAction("Index", "Home");
        }

        // иначе показываем страницу входа
        return View(new LoginViewModel { ReturnUrl = returnUrl });
    }
    
    [Route("Login")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await userService.CheckPasswordAsync(model.Email, model.Password);

            if (result)
            {
                await userService.SignInAsync(model.Email, false);
                return RedirectToAction("MyPage", "AccountManager");
            }

            ModelState.AddModelError("", "Неправильный логин и (или) пароль");
        }
        return RedirectToAction("Index", "Home");
    }
    
    [Route("Logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await userService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [Authorize]
    [Route("MyPage")]
    [HttpGet]
    public async Task<IActionResult> MyPage()
    {
        var user = User;

        var result = await userService.GetUserAsync(user);

        var model = new UserViewModel(result);

        model.Friends = friendService.GetFriendsByUser(model.User);

        return View("MyPage", model);
    }

    [Route("Edit")]
    [HttpGet]
    public IActionResult Edit()
    {
        var user = User;

        var result = userService.GetUserAsync(user);

        var editmodel = mapper.Map<UserEditViewModel>(result.Result);

        return View("EditUserProfile", editmodel);
    }

    [Authorize]
    [Route("Update")]
    [HttpPost]
    public async Task<IActionResult> Update(UserEditViewModel model)
    {
        if (ModelState.IsValid)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userService.GetUserByIdAsync(userId);

            user.Convert(model);

            var result = await userService.UpdateAsync(user);

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

        ModelState.AddModelError("", "Некорректные данные");
        return View("EditUserProfile", model);
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
        friendService.AddFriend(User, id);

        return RedirectToAction("MyPage", "AccountManager");
    }

    [Route("DeleteFriend")]
    [HttpPost]
    public async Task<IActionResult> DeleteFriend(string id)
    {
        friendService.DeleteFriend(User, id);

        return RedirectToAction("MyPage", "AccountManager");
    }

    private async Task<SearchViewModel> CreateSearch(string search)
    {
        var currentUser = User;
        var currentUserEntity = await userService.GetUserAsync(currentUser);

        // Получаем пользователей, подходящих под поиск
        var list = userService.GetUsersForSearch(search, currentUserEntity.Id);

        // Получаем друзей текущего пользователя
        var friends = friendService.GetFriendsByUser(currentUserEntity);

        var data = list.Select(x =>
        {
            var t = mapper.Map<UserWithFriendExt>(x);

            // Проверка дружбы
            t.IsFriendWithCurrent = friends.Any(y => y.Id == x.Id || x.Id == currentUserEntity.Id);

            // Безопасный full name для вьюхи
            t.FullName = string.Join(" ", new[] { x.LastName, x.FirstName, x.MiddleName }
                .Where(s => !string.IsNullOrEmpty(s)));

            return t;
        }).ToList();

        var model = new SearchViewModel()
        {
            UserList = data
        };

        return model;
    }

    [Route("Chat")]
    [HttpPost]
    public async Task<IActionResult> Chat(string id)
    {
        var model = await GenerateChat(id);
        return View("Chat", model);
    }

    private async Task<ChatViewModel> GenerateChat(string id)
    {
        // Получаем DAL-сущности
        var currentUserEntity = await userManager.GetUserAsync(User);
        var friendEntity = await userManager.FindByIdAsync(id);

        // Маппим в BLL-модели
        var currentUser = mapper.Map<User>(currentUserEntity);
        var friend = mapper.Map<User>(friendEntity);

        // Берем сообщения
        var repository = unitOfWork.GetRepository<Message>() as MessageRepository;
        var messages = repository.GetMessages(currentUserEntity, friendEntity); // здесь можно оставить DAL-сущности

        var model = new ChatViewModel
        {
            You = currentUser,
            ToWhom = friend,
            History = messages.Select(m => mapper.Map<Message>(m)).OrderBy(x => x.Id).ToList()
        };

        return model;
    }

    [Route("Chat")]
    [HttpGet]
    public async Task<IActionResult> Chat()
    {

        var id = Request.Query["id"];

        var model = await GenerateChat(id);
        return View("Chat", model);
    }

    [Route("NewMessage")]
    [HttpPost]
    public async Task<IActionResult> NewMessage(string id, ChatViewModel chat)
    {
        var currentUserEntity = await userManager.GetUserAsync(User);
        var friendEntity = await userManager.FindByIdAsync(id);

        // Создаем сообщение (DAL)
        var repository = unitOfWork.GetRepository<Message>() as MessageRepository;
        var sender = mapper.Map<User>(currentUserEntity);
        var recipient = mapper.Map<User>(friendEntity);
        var item = new Message
        {
            Sender = sender,
            Recipient = recipient,
            Text = chat.NewMessage.Text
        };
        // Map BLL Message to DAL MessageEntity before saving
        var messageEntity = mapper.Map<MessageEntity>(item);
        repository?.Create(messageEntity);
        unitOfWork.SaveChanges();

        // Генерируем обновленный чат (BLL-модели)
        var model = await GenerateChat(id);
        return View("Chat", model);
    }
    
}