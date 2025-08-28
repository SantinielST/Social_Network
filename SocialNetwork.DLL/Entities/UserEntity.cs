using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.DLL.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public DateTime BirthDate { get; set; }

    public string? Image { get; set; }

    public string? Status { get; set; }

    public string? About { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + " " + LastName;
    }
}
