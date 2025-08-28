using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Repositories.Base;

namespace SocialNetwork.DLL.Repositories;

public class FriendsRepository : Repository<FriendEntity>
{
    public FriendsRepository(ApplicationDbContext db) : base(db)
    {

    }

    public void AddFriend(UserEntity target, UserEntity Friend)
    {
        var friends = Set.AsEnumerable().FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);

        if (friends == null)
        {
            var item = new FriendEntity()
            {
                UserId = target.Id,
                User = target,
                CurrentFriend = Friend,
                CurrentFriendId = Friend.Id,
            };

            Create(item);
        }
    }

    public List<UserEntity> GetFriendsByUser(UserEntity target)
    {
        var friends = Set.Include(x => x.CurrentFriend).Include(x => x.User).AsEnumerable().Where(x => x.User.Id == target.Id).Select(x => x.CurrentFriend);

        return friends.ToList();
    }

    public void DeleteFriend(UserEntity target, UserEntity Friend)
    {
        var friends = Set.AsEnumerable().FirstOrDefault(x => x.UserId == target.Id && x.CurrentFriendId == Friend.Id);

        if (friends != null)
        {
            Delete(friends);
        }
    }

}