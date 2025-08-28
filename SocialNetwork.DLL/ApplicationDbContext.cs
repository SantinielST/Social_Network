using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL.Entities;

namespace SocialNetwork.DLL;

public class ApplicationDbContext : IdentityDbContext<UserEntity>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<UserEntity> userEntities { get; set; }
}
