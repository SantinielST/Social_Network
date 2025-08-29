using AutoMapper;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.UoW;

namespace SocialNetwork.BLL.Services;

public class FriendService(IUnitOfWork unitOfWork, IMapper mapper)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public List<User> GetFriendsByUser(User user)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;
        var userEntity = _mapper.Map<UserEntity>(user);

        var listUsersEntity = repository.GetFriendsByUser(userEntity);

        return [.. listUsersEntity.Select(u => _mapper.Map<User>(u))];
    }

    public void AddFriend(User user, User friend)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        repository.AddFriend(_mapper.Map<UserEntity>(user), mapper.Map<UserEntity>(friend));
    }

    public void DeleteFriend(User user, User friend)
    {
        var repository = _unitOfWork.GetRepository<FriendEntity>() as FriendsRepository;

        repository.DeleteFriend(_mapper.Map<UserEntity>(user), _mapper.Map<UserEntity>(friend));
    }
}
