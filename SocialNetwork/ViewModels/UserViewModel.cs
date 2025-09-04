using SocialNetwork.BLL.Models;

namespace SocialNetwork.ViewModels;

public class UserViewModel
{
    public string Id => User.Id.ToString();
    public string Email { get; set; }
    public User User { get; set; }
    public List<User> Friends { get; set; } = new();

    public UserViewModel() { } //для автом. Model Binding нужен пустой конструктор.

    public UserViewModel(User user)
    {
        User = user;
    }
}