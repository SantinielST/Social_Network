namespace SocialNetwork.DLL.Entities;

public class MessageEntity
{
    public int Id { get; set; }
    public string Text { get; set; }

    public string SenderId { get; set; }
    public UserEntity Sender { get; set; }

    public string RecipientId { get; set; }
    public UserEntity Recipient { get; set; }
}