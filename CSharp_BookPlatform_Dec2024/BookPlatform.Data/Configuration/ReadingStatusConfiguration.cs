using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using BookPlatform.Data.Models;

namespace BookPlatform.Data.Configuration
{
    public class ReadingStatusConfiguration : IEntityTypeConfiguration<ReadingStatus>
    {
        public void Configure(EntityTypeBuilder<ReadingStatus> builder)
        {
            builder.HasData(this.Seed());
        }

        protected IEnumerable<ReadingStatus> Seed()
        {
            IEnumerable<ReadingStatus> readingStatuses = new List<ReadingStatus>()
            {
                new ReadingStatus()
                {
                    Id = 1,
                    StatusDescription = "Read"
                },
                new ReadingStatus()
                {
                    Id = 2,
                    StatusDescription = "Currently Reading"
                },
                new ReadingStatus()
                {
                    Id = 3,
                    StatusDescription = "Want to Read"
                },
            };

            return readingStatuses;
        }
    }
}
