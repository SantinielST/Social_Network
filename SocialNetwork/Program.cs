using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DLL;
using SocialNetwork.DLL.Repositories;
using SocialNetwork.DLL.Entities;
using SocialNetwork.DLL.Interfaces;

namespace SocialNetwork;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string connection = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connection));


        builder.Services.AddIdentity<UserEntity, IdentityRole>(opts =>
        {
            opts.Password.RequiredLength = 11;
            opts.Password.RequireNonAlphanumeric = true;
            opts.Password.RequireLowercase = true;
            opts.Password.RequireUppercase = true;
            opts.Password.RequireDigit = true;
        })
                .AddEntityFrameworkStores<ApplicationDbContext>();

        //builder.Services.AddScoped<IUnitOfWork>();
        builder.Services.AddScoped<IRepository<FriendEntity>, FriendsRepository>();
        builder.Services.AddAutoMapper((v) => v.AddProfile(new MappingProfile()));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}