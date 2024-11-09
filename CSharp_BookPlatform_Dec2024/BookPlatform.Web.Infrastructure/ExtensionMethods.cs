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

            PlatformDbContext context = serviceScope.ServiceProvider.GetRequiredService<PlatformDbContext>()!;
            context.Database.MigrateAsync();

            return app;
        }
    }
}
