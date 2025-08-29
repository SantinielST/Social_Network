namespace SocialNetwork.DLL.Entities;

public class FriendEntity
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public UserEntity User { get; set; }

    public string CurrentFriendId { get; set; }

    public UserEntity CurrentFriend { get; set; }
}
