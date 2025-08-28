using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.BLL.Services
{
    public class UserService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserEntity> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var entity = await _userManager.FindByIdAsync(id);
            return _mapper.Map<User>(entity);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var entity = _mapper.Map<UserEntity>(user);
            return await _userManager.CreateAsync(entity, password);
        }

        public async Task<UserEntity> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
