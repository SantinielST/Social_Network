using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.ViewModels;

public class UserViewModel
{
    public UserEntity User { get; set; }

    public UserViewModel(UserEntity user)
    {
        User = user;
    }

    public List<UserEntity> Friends { get; set; }
}