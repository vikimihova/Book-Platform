using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using BookPlatform.Data;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Web.Infrastructure.Extensions
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            PlatformDbContext context = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;
            context.Database.MigrateAsync();

            return app;
        }

        public static IApplicationBuilder SeedDatabase(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            PlatformDbContext context = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;
            context.SeedAuthors();
            context.SeedGenres();
            context.SeedBooks();
            context.SeedCharacters();

            return app;
        }

        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            PlatformDbContext context = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;

            // update book image URL
            context.UpdateBooksImageUrl();

            return app;
        }
              
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app, string email, string username, string password)
        {
            // create scope
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();

            // get role manager
            RoleManager<ApplicationRole>? roleManager = serviceScope
                .ServiceProvider
                .GetService<RoleManager<ApplicationRole>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager),
                    $"Service for {typeof(RoleManager<ApplicationRole>)} cannot be obtained!");
            }

            // get user store
            IUserStore<ApplicationUser>? userStore = serviceScope
                .ServiceProvider
                .GetService<IUserStore<ApplicationUser>>();

            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore),
                    $"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
            }

            // get user manager
            UserManager<ApplicationUser>? userManager = serviceScope
                .ServiceProvider
                .GetService<UserManager<ApplicationUser>>();    

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager),
                    $"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
            }
                        
            Task.Run(async () =>
            {
                // check if role User already exists and create if necessary
                bool userRoleExists = await roleManager.RoleExistsAsync(UserRoleName);
                ApplicationRole? userRole = null;

                if (!userRoleExists)
                {
                    userRole = new ApplicationRole(UserRoleName);

                    IdentityResult result = await roleManager.CreateAsync(userRole);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {UserRoleName} role!");
                    }
                }
                else
                {
                    userRole = await roleManager.FindByNameAsync(UserRoleName);
                }

                // check if role Admin already exists and create if necessary
                bool adminRoleExists = await roleManager.RoleExistsAsync(AdminRoleName);
                ApplicationRole? adminRole = null;

                if (!adminRoleExists)
                {
                    adminRole = new ApplicationRole(AdminRoleName);

                    IdentityResult result = await roleManager.CreateAsync(adminRole);

                    if (!result.Succeeded)
                    {
                        throw new InvalidOperationException($"Error occurred while creating the {AdminRoleName} role!");
                    }
                }
                else
                {
                    adminRole = await roleManager.FindByNameAsync(AdminRoleName);
                }

                // check if admin user already exists and create if necessary
                ApplicationUser? adminUser = await userManager.FindByEmailAsync(email);

                if (adminUser == null)
                {
                    adminUser = await
                        CreateAdminUserAsync(email, username, password, userStore, userManager);
                }

                // check if admin user already in role Admin and add to role if necessary
                if (await userManager.IsInRoleAsync(adminUser, AdminRoleName))
                {
                    return app;
                }

                IdentityResult userResult = await userManager.AddToRoleAsync(adminUser, AdminRoleName);

                if (!userResult.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while adding the user {username} to the {AdminRoleName} role!");
                }

                return app;
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        private static async Task<ApplicationUser> CreateAdminUserAsync(string email, string username, string password,
            IUserStore<ApplicationUser> userStore, UserManager<ApplicationUser> userManager)
        {
            // create user with email
            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = email
            };

            // set username
            await userStore.SetUserNameAsync(applicationUser, username, CancellationToken.None);

            // add user and set password
            IdentityResult result = await userManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error occurred while registering {AdminRoleName} user!");
            }

            return applicationUser;
        }
    }
}
