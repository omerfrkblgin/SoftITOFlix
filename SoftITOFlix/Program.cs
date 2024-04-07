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
            Restricton restricton;
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
                    if(context.Restrictons.Count() == 0)
                    {
                        restricton = new Restricton();
                        restricton.Id = 0;
                        restricton.Name = "Genel Ýzleyici";
                        context.Restrictons.Add(restricton);
                        restricton = new Restricton();
                        restricton.Id = 7;
                        restricton.Name = "7 yaþ ve üzeri";
                        context.Restrictons.Add(restricton);
                        restricton = new Restricton();
                        restricton.Id = 13;
                        restricton.Name = "13 yaþ ve üzeri";
                        context.Restrictons.Add(restricton);
                        restricton = new Restricton();
                        restricton.Id = 18;
                        restricton.Name = "18 yaþ ve üzeri";
                        context.Restrictons.Add(restricton);
                        restricton = new Restricton();
                        restricton.Id = 19;
                        restricton.Name = "Olumsuz örnek";
                        context.Restrictons.Add(restricton);
                    }
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
                            user.Name = "Admin";
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
