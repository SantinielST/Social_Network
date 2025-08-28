using SocialNetwork.BLL.Models;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.ViewModels;

public class SearchViewModel
{
    public List<UserWithFriendExt> UserList { get; set; }
}
