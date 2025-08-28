using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.DLL.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }
}
