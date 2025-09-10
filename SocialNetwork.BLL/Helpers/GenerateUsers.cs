using SocialNetwork.BLL.Models;

namespace SocialNetwork.BLL.Helpers;

public class GenerateUsers
{
    public List<User> Populate(int n)
    {
        var users = new List<User>();

        for (int i = 0; i < n; i++)
        {
            var user = new User()
            {
                FirstName = "FirstName_" + i.ToString(),
                LastName = "LastName_" + i.ToString(),
                BirthDate = DateTime.Parse("01.01.2000"),
                Email = "email@email." + i.ToString(),
                UserName = "email@email." + i.ToString(),
            };
            users.Add(user);
        }
        return users;
    }
}
