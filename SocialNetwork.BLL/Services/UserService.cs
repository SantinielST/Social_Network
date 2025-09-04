using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        var userEntity = await _userManager.FindByIdAsync(id);
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

    public async Task<IdentityResult> UpdateAsync(User userFromModel, string Id)
    {
        var userEntity = await _userManager.FindByIdAsync(Id);

        userEntity.Image = userFromModel.Image;
        userEntity.LastName = userFromModel.LastName;
        userEntity.FirstName = userFromModel.FirstName;
        userEntity.MiddleName = userFromModel.MiddleName;
        userEntity.Email = userFromModel.Email;
        userEntity.UserName = userFromModel.Email;
        userEntity.Status = userFromModel.Status;
        userEntity.About = userFromModel.About;

        return await _userManager.UpdateAsync(userEntity);
    }

    public List<User> GetUsersForSearch(string search, string Id)
    {
        var userListEntity = new List<UserEntity>();

        if (string.IsNullOrEmpty(search))
        {
            userListEntity = _userManager.Users.AsEnumerable().Where(u => u.Id != Id).ToList();
        }
        else
        {
            userListEntity = _userManager.Users.AsEnumerable().Where(x => x.GetFullName().ToLower().Contains(search.ToLower())).ToList();
        }

        return [.. userListEntity.Select(u => _mapper.Map<User>(u))];
    }
}