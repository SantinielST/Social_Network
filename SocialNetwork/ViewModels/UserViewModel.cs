using SocialNetwork.BLL.Models;

namespace SocialNetwork.ViewModels;

public class UserViewModel
{
    public string Id => User.Id.ToString();
    public string Email { get; set; }
    public User User { get; set; }
    public List<User> Friends { get; set; } = new();

    public UserViewModel() { } //Model Binding �ڧ���ݧ�٧�֧� ������� �ܧ�ߧ����ܧ���.

    public UserViewModel(User user)
    {
        User = user;
    }
}