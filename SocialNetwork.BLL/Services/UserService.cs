using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;

public class UserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IMapper _mapper;
    private readonly SignInManager<UserEntity> _signInManager;

    public UserService(UserManager<UserEntity> userManager, IMapper mapper, SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
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

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<UserEntity?> GetUserEntityByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }
}