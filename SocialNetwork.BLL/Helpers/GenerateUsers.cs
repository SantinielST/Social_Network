using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Helpers;

public class GenerateUsers
{
    public List<User> Populate(int n)
    {
        var users = new List<User>();
        var random = new Random();

        for (int i = 0; i < n; i++)
        {
            var value = random.Next(0, 100000);
            var user = new User()
            {
                FirstName = "FirstName_" + value,
                LastName = "LastName_" + value,
                BirthDate = DateTime.Parse("01.01.2000"),
                Email = "email@email." + value,
                UserName = "email@email." + value,
            };
            users.Add(user);
        }
        return users;
    }
}