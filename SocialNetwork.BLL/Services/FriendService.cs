using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.UoW;
using System.Security.Claims;

namespace SocialNetwork.BLL.Services;

public class FriendService(IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<UserEntity> _userManager)
{

    public async Task<List<User>> GetFriendsByUser(User user)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;
        var userEntity = _mapper.Map<UserEntity>(user);

        var listUsersEntity = await repository.GetFriendsByUser(userEntity);

        return [.. listUsersEntity.Select(u => _mapper.Map<User>(u))];
    }

    public async Task AddFriend(ClaimsPrincipal userRequester, string friendId)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        var user = await _userManager.GetUserAsync(userRequester);
        var friend = await _userManager.FindByIdAsync(friendId);

        await repository.AddFriend(user, friend);
    }

    public async Task DeleteFriend(ClaimsPrincipal userRequester, string friendId)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        var user = await _userManager.GetUserAsync(userRequester);
        var friend = await _userManager.FindByIdAsync(friendId);

        await repository.DeleteFriend(user, friend);
    }
}
