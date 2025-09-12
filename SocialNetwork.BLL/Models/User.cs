using Microsoft.AspNetCore.Identity;

namespace SocialNetwork.BLL.Models;

public class User : IdentityUser
{
    //IdentityUser уже содержит собственное поле Id (string Id) и Email (EmailReg).

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? MiddleName { get; set; }

    public string Password { get; set; }

    public DateTime BirthDate { get; set; }
    
    public string Image { get; set; }

    public string Status { get; set; }

    public string About { get; set; }

    public string GetFullName()
    {
        return FirstName + " " + MiddleName + " " + LastName;
 
    }
    public User()
    {
        Image = "https://thispersondoesnotexist.com";
        Status = "Ура! Я в соцсети!";
        About = "Информация обо мне.";
    }
}