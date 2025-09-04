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
        var query = Set.Include(x => x.Recipient)
            .Include(x => x.Sender);

        var from = query.Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToList();
        var to = query.Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToList();

        return from.Concat(to).OrderBy(x => x.Id).ToList();
    }
}