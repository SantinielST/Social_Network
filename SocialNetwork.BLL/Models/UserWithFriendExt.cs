using SocialNetwork.DLL.Entities;

namespace SocialNetwork.BLL.Models;

public class UserWithFriendExt : UserEntity
{
    public bool IsFriendWithCurrent { get; set; }
}
