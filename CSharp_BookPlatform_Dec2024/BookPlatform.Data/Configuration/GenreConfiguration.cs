using BookPlatform.Data.Models;
using BookPlatform.Services.Data.DataProcessor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookPlatform.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            //builder.HasData(Seed());
        }

        protected IEnumerable<Genre> Seed()
        {
            // SEED INITIAL DATA FROM JSON FILE
            // IEnumerable<Genre> genresToAdd = Deserializer.GenerateGenres();

            // SEED ADDITIONAL DATA
            IEnumerable<Genre> genresToAdd = new List<Genre>()
            {
            };

            return genresToAdd;
        }
    }
}
