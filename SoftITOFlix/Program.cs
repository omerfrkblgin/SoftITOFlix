using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoftITOFlix.Data;
using Microsoft.AspNetCore.Identity;
using SoftITOFlix.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SoftITOFlix
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SoftITOFlixRole softITOFlixRole;
            SoftITOFlixUser user;

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<SoftITOFlixContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SoftITOFlixContext") ?? throw new InvalidOperationException("Connection string 'SoftITOFlixContext' not found.")));

            builder.Services.AddIdentity<SoftITOFlixUser, SoftITOFlixRole>()
            .AddEntityFrameworkStores<SoftITOFlixContext>().AddDefaultTokenProviders();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();
            {
                SoftITOFlixContext? context = app.Services.CreateScope().ServiceProvider.GetService<SoftITOFlixContext>();
                if (context != null)
                {
                    context.Database.Migrate();
                    context.SaveChanges();
                    RoleManager<SoftITOFlixRole>? roleManager = app.Services.CreateScope().ServiceProvider.GetService<RoleManager<SoftITOFlixRole>>();
                    if (roleManager != null)
                    {
                        if (roleManager.Roles.Count() == 0)
                        {
                            softITOFlixRole = new SoftITOFlixRole("Admin");
                            roleManager.CreateAsync(softITOFlixRole).Wait();
                            softITOFlixRole = new SoftITOFlixRole ("ContentAdmin");
                            roleManager.CreateAsync(softITOFlixRole).Wait();

                        }
                    }
                    UserManager<SoftITOFlixUser>? userManager = app.Services.CreateScope().ServiceProvider.GetService<UserManager<SoftITOFlixUser>>();
                    if (userManager != null)
                    {
                        if (userManager.Users.Count() == 0)
                        {
                            user = new SoftITOFlixUser();
                            user.UserName = "Admin";
                            user.Name = "Administrator";
                            user.Email = "admin@gmail.com";
                            user.PhoneNumber = "5023424232";
                            user.BirthDate = DateTime.Today;
                            user.Passive = false;
                            userManager.CreateAsync(user, "Admin123!").Wait();
                            userManager.AddToRoleAsync(user, "Admin").Wait();

                        }
                    }
                }

            }

            app.Run();
        }
    }
}
