using BookPlatform.Data.Models;
using BookPlatform.Data.Repository;
using BookPlatform.Services.Data.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Runtime.CompilerServices;

namespace BookPlatform.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasData(Deserializer.GenerateAuthors());    
        }        
    }
}
