using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.UoW;
using System.Security.Claims;

namespace SocialNetwork.BLL.Services;

public class MessageService(UserManager<UserEntity> userManager, IUnitOfWork unitOfWork, IMapper mapper)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<(List<Message>, User, User)> GetMessagesAsync(ClaimsPrincipal user, string id)
    {
        var currentUser = await _userManager.GetUserAsync(user);
        var friend = await _userManager.FindByIdAsync(id);

        var repository = _unitOfWork.GetRepository<MessageEntity>() as MessageRepository;
        var messages = await repository.GetMessages(currentUser, friend);

        return ([.. messages.Select(m => _mapper.Map<Message>(m))], _mapper.Map<User>(currentUser), _mapper.Map<User>(friend));
    }

    public async Task NewMessageAsync(ClaimsPrincipal user, string text, string id)
    {
        var currentUserEntity = await _userManager.GetUserAsync(user);
        var friendEntity = await _userManager.FindByIdAsync(id);

        // Создаем сообщение (DAL)
        var repository = _unitOfWork.GetRepository<MessageEntity>() as MessageRepository;

        var item = new MessageEntity
        {
            Sender = currentUserEntity,
            Recipient = friendEntity,
            Text = text
        };
        // Map BLL Message to DAL MessageEntity before saving

        await repository?.Create(item);

        await unitOfWork.SaveChanges();
    }
}