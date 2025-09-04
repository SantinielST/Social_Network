namespace SocialNetwork.BLL.Models;

public class UserWithFriendExt 
{
    public User? User { get; init; } // убираем наследование от UserEntity
    public bool IsFriendWithCurrent { get; set; }
    public string FullName { get; set; }

    public string GetFullName()
    {
        if (User == null) return string.Empty;

        // Складываем FirstName, MiddleName и LastName безопасно для null
        return string.Join(" ", new[] { User.FirstName, User.MiddleName, User.LastName }
                             .Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}
