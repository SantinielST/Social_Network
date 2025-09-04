using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.UoW;
using System.Security.Claims;

namespace SocialNetwork.BLL.Services;

public class FriendService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<UserEntity> userManager)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public List<User> GetFriendsByUser(User user)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;
        var userEntity = _mapper.Map<UserEntity>(user);

        var listUsersEntity = repository.GetFriendsByUser(userEntity);

        return [.. listUsersEntity.Select(u => _mapper.Map<User>(u))];
    }

    public void AddFriend(ClaimsPrincipal userRequester, string friendId)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        var user = _userManager.GetUserAsync(userRequester).Result;
        var friend = _userManager.FindByIdAsync(friendId).Result;

        repository.AddFriend(user, friend);
    }

    public void DeleteFriend(ClaimsPrincipal userRequester, string friendId)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        var user = _userManager.GetUserAsync(userRequester).Result;
        var friend = _userManager.FindByIdAsync(friendId).Result;

        repository.DeleteFriend(user, friend);
    }
}
