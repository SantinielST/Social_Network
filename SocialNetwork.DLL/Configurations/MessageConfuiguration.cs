using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.DLL.Configurations;

public class MessageConfuiguration : IEntityTypeConfiguration<MessageEntity>
{
    public void Configure(EntityTypeBuilder<MessageEntity> builder)
    {
        builder.ToTable("Mesages").HasKey(p => p.Id);
        builder.Property(x => x.Id).UseIdentityColumn();
    }
}