using BookPlatform.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            // ADD SERVICES TO THE CONTAINER
            // Add dbContext
            }); // !NuGet Microsoft Extensions Dependency Injection package!
            // Add db developer page exception filter (only in development environment)
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Add identity
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            // Configure application cookie
            // Add repositories for each entity (repository pattern) except for ApplicationUser (UserManager and SignInManager instead)
            // Add services for controllers
            // Add other services
            builder.Services.AddControllersWithViews();

            // BUILD APPLICATION
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            // ADD AUTOMAPPER

            // CONFIGURE THE HTTP REQUEST PIPELINE

            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            // APPLY MIGRATIONS

            // RUN APPLICATION
            app.Run();
        }
    }
}
