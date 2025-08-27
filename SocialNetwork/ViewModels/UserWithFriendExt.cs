using SocialNetwork.BLL.Models;

namespace SocialNetwork.ViewModels;

public class UserWithFriendExt : User
{
        public bool IsFriendWithCurrent { get; set; }
        
        public string GetFullName() => $"{FirstName} {LastName}";
}