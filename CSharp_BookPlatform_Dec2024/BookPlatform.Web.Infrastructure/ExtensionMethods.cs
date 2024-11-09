using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using BookPlatform.Data;

namespace BookPlatform.Web.Infrastructure
{
    public static class ExtensionMethods
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();

            PlatformDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;
            dbContext.Database.MigrateAsync();

            return app;
        }
    }
}
