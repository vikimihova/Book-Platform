using Microsoft.EntityFrameworkCore;
using BookPlatform.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(au => au.Friends)
                .WithMany();
        }
    }
}
