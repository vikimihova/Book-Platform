using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BookPlatform.Data.Models;

namespace BookPlatform.Data.Configuration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasData(this.Seed());
        }

        protected IEnumerable<Rating> Seed()
        {
            IEnumerable<Rating> ratings = new List<Rating>()
            {
                new Rating()
                {
                    Id = 1,
                    RatingDescription = "Barely finished it, if at all"
                },
                new Rating()
                {
                    Id = 2,
                    RatingDescription = "Not much to enjoy/ learn"
                },
                new Rating()
                {
                    Id = 3,
                    RatingDescription = "Good parts are good, bad parts are bad"
                },
                new Rating()
                {
                    Id = 4,
                    RatingDescription = "Would definitely recommend/ reread"
                },
                new Rating()
                {
                    Id = 5,
                    RatingDescription = "Absolutely amazing"
                },
            };

            return ratings;
        }
    }
}
