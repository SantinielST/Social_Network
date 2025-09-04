using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories.Base;

namespace SocialNetwork.DLL.Repositories;

public class MessageRepository : Repository<MessageEntity>
{
    public MessageRepository(ApplicationDbContext db)
        : base(db)
    {

    }

    public List<MessageEntity> GetMessages(UserEntity sender, UserEntity recipient)
    {
        Set.Include(x => x.Recipient);
        Set.Include(x => x.Sender);

        var from = Set.AsEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
        var to = Set.AsEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();

        var itog = new List<MessageEntity>();
        itog.AddRange(from);
        itog.AddRange(to);
        itog.OrderBy(x => x.Id);
        return itog;
    }
}