using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories.Base;

namespace SocialNetwork.DLL.Repositories;

public class FriendsRepository(ApplicationDbContext db) : Repository<FriendEntity>(db)
{
    public void AddFriend(UserEntity target, UserEntity friend)
    {
        var existing = Set.FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

        if (existing == null)
        {
            var item = new FriendEntity()
            {
                UserId = target.Id,
                User = target,
                CurrentFriend = friend,
                CurrentFriendId = friend.Id,
            };

            Create(item);
        }
    }

    public List<UserEntity> GetFriendsByUser(UserEntity target)
    {
        var friends = Set.Include(x => x.CurrentFriend).Include(x => x.User).AsEnumerable().Where(x => x.User.Id == target.Id).Select(x => x.CurrentFriend);

        return friends.ToList();
    }

    public void DeleteFriend(UserEntity target, UserEntity friend)
    {
        var existing = Set.FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == friend.Id);

        if (existing != null)
        {
            Delete(existing);
        }
    }
}