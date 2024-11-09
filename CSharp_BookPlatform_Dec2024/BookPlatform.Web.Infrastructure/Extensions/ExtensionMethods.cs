using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using BookPlatform.Data;

namespace BookPlatform.Web.Infrastructure.Extensions
{
    public static class ExtensionMethods
    {
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

        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            PlatformDbContext context = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;
            context.Database.MigrateAsync();

            return app;
        }
    }
}
