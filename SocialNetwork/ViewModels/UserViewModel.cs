using SocialNetwork.BLL.Models;

namespace SocialNetwork.ViewModels;

public class UserViewModel
{
    public string Id => User.Id;
    public string Email { get; set; }
    public User User { get; set; }
    public List<User> Friends { get; set; } = new();

    public UserViewModel() { } //Model Binding §Ú§ã§á§à§Ý§î§Ù§å§Ö§ä §á§å§ã§ä§à§Û §Ü§à§ß§ã§ä§â§å§Ü§ä§à§â.

    public UserViewModel(User user)
    {
        User = user;
    }
}