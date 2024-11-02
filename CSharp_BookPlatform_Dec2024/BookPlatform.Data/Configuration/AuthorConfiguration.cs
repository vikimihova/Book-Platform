using BookPlatform.Data.Models;
using BookPlatform.Services.Data.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(Seeder.GenerateAuthors());
        }               
    }
}
