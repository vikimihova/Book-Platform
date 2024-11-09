using BookPlatform.Data;
using BookPlatform.Data.Models;
using BookPlatform.Services.Data.Interfaces;
using BookPlatform.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("SqlServer");

            // ADD SERVICES TO THE CONTAINER

            // Add dbContext
            builder.Services.AddDbContext<PlatformDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging() // delete!
                    .LogTo(Console.WriteLine, LogLevel.Information); // delete!
            }); // !NuGet Microsoft Extensions Dependency Injection package!
            


            // Add db developer page exception filter (only in development environment)
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add identity     
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireDigit = builder.Configuration.GetValue<bool>("Identity:Password:RequireDigits");
                options.Password.RequireLowercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireLowercase");
                options.Password.RequireUppercase = builder.Configuration.GetValue<bool>("Identity:Password:RequireUppercase");
                options.Password.RequireNonAlphanumeric = builder.Configuration.GetValue<bool>("Identity:Password:RequireNonAlphanumeric");
                options.Password.RequiredUniqueChars = builder.Configuration.GetValue<int>("Identity:Password:RequireUniqueCharacters");
                options.Password.RequiredLength = builder.Configuration.GetValue<int>("Identity:Password:RequireLength");

                options.SignIn.RequireConfirmedAccount = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedAccount");
                options.SignIn.RequireConfirmedEmail = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedEmail");
                options.SignIn.RequireConfirmedPhoneNumber = builder.Configuration.GetValue<bool>("Identity:SignIn:RequireConfirmedPhoneNumber");

                options.User.RequireUniqueEmail = builder.Configuration.GetValue<bool>("Identity:User:RequireUniqueEmail");
            })
            .AddEntityFrameworkStores<PlatformDbContext>()
            .AddRoles<IdentityRole<Guid>>()
            .AddSignInManager<SignInManager<ApplicationUser>>()
            .AddUserManager<UserManager<ApplicationUser>>();


            // Configure application cookie
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
            });

            // Add repositories for each entity (repository pattern) except for ApplicationUser (UserManager and SignInManager instead)
            builder.Services.RegisterRepositories(typeof(ApplicationUser).Assembly);

            // Add services for controllers
            builder.Services.RegisterUserDefinedServices(typeof(IBaseService).Assembly);

            // Add other services
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // builder.Services.AddHttpClient<OpenLibraryService>();

            // BUILD APPLICATION
            var app = builder.Build();

            // CONFIGURE THE HTTP REQUEST PIPELINE

            // ADD AUTOMAPPER

            if (app.Environment.IsDevelopment())            
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/StatusCode");
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

            app.MapRazorPages();

            // SEED DATABASE
            app.SeedDatabase();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var context = scope
            //      .ServiceProvider
            //      .GetRequiredService<PlatformDbContext>();               
            //}

            // UPDATE DATABASE
            // ...

            // APPLY MIGRATIONS
            app.ApplyMigrations();

            // RUN APPLICATION
            app.Run();
        }
    }
}
