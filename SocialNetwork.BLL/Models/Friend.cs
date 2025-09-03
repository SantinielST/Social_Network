namespace SocialNetwork.BLL.Models;

public class Friend //DTO для передачи данных в сервисах, API или контроллерах.
                    //Использует User из BLL, а не UserEntity.
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }

    public string CurrentFriendId { get; set; }

    public User CurrentFriend { get; set; }
}