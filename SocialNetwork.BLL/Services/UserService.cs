using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.BLL.Helpers;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL;
using SocialNetwork.DLL.Entities;
using System.Security.Claims;

namespace SocialNetwork.BLL.Services;

public class UserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ApplicationDbContext _applicationDbContext;

    public UserService(UserManager<UserEntity> userManager, IMapper mapper, SignInManager<UserEntity> signInManager, ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<User?> GetUserAsync(ClaimsPrincipal user)
    {
        var userEntity = await _userManager.GetUserAsync(user);
        return _mapper.Map<User>(userEntity);
    }

    public async Task<User?> GetUserByIdAsync(string id)
    {
        var userEntity = await _userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return userEntity != null ? _mapper.Map<User>(userEntity) : null;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var userEntity = await _userManager.FindByEmailAsync(email);
        return userEntity != null ? _mapper.Map<User>(userEntity) : null;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        var userEntity = _mapper.Map<UserEntity>(user);

        // сбрасываем Kind именно у entity, которое уходит в БД
        userEntity.BirthDate = DateTime.SpecifyKind(userEntity.BirthDate, DateTimeKind.Unspecified);

        return await _userManager.CreateAsync(userEntity, password);
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var userEntity = await _userManager.FindByEmailAsync(email);
        if (userEntity == null) return false;
        return await _userManager.CheckPasswordAsync(userEntity, password);
    }

    public async Task SignInAsync(string email, bool isPersistent)
    {
        var userEntity = await _userManager.FindByEmailAsync(email);
        if (userEntity != null)
            await _signInManager.SignInAsync(userEntity, isPersistent);
    }

    public bool IsSignIn(ClaimsPrincipal user)
    {
        return _signInManager.IsSignedIn(user);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<UserEntity?> GetUserEntityByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> UpdateAsync(User user)
    {
        var userEntity = await _userManager.FindByIdAsync(user.Id); // трекаемый объект
        _mapper.Map(user, userEntity); // обновляем свойства существующего объекта, убрала ручной маппинг
        return await _userManager.UpdateAsync(userEntity);
    }

    public async Task<List<User>> GetUsersForSearch(string search, string currentUserId)
    {
        IQueryable<UserEntity> query = _userManager.Users;

        // исключаем текущего пользователя
        query = query.Where(u => u.Id != currentUserId);

        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();

            query = query.Where(u =>
                EF.Functions.ILike(u.FirstName, $"%{search}%") ||
                EF.Functions.ILike(u.LastName, $"%{search}%") ||
                (u.MiddleName != null && EF.Functions.ILike(u.MiddleName, $"%{search}%"))
            );
        }

        var userListEntity = await query.ToListAsync();

        return userListEntity.Select(u => _mapper.Map<User>(u)).ToList();
    }

    public async Task GenerateUsersAsync(int n)
    {
        var usergen = new GenerateUsers();
        var userlist = usergen.Populate(n);

        foreach (var user in userlist)
        {
            var result = await _userManager.CreateAsync(_mapper.Map<UserEntity>(user), "Qxwzsa243-Qxwzsa243");

            if (!result.Succeeded)
                continue;
        }
    }
}