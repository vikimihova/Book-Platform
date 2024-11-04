using BookPlatform.Data.Migrations;
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
            //builder.HasData(Seed());
        }
        
        protected IEnumerable<Author> Seed()
        {
            // SEED INITIAL DATA FROM JSON FILE
            //IEnumerable<Author> authorsToAdd = Deserializer.GenerateAuthors();

            // SEED ADDITIONAL DATA
            IEnumerable<Author> authorsToAdd = new List<Author>()
            {
                new Author()
                {
                    FullName = "Douglas Adams",
                    FirstName = "Douglas",
                    LastName = "Adams"
                }
            };

            return authorsToAdd;
        }
    }
}
