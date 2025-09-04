using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.DLL.Configurations;

public class FriendConfiguration : IEntityTypeConfiguration<FriendEntity>
{

    public void Configure(EntityTypeBuilder<FriendEntity> builder)
    {
        builder.ToTable("UserFriends").HasKey(p => p.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
    }
}