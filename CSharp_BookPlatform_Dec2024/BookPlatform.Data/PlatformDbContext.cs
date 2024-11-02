using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BookPlatform.Data
{
    public class PlatformDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options)
        {            
        }

        public virtual DbSet<Author> Authors { get; set; }

        public virtual DbSet<Genre> Genres { get; set; }

        public virtual DbSet<Book> Books { get; set; }

        public virtual DbSet<BookApplicationUser> BooksApplicationUsers { get; set; }

        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Quote> Quotes { get; set; }

        public virtual DbSet<Rating> Ratings { get; set; }

        public virtual DbSet<ReadingStatus> ReadingStatuses { get; set; }  

        public virtual DbSet<QuoteApplicationUser> QuotesApplicationUsers { get; set; }

        public virtual DbSet<Character> Characters { get; set; }

        public virtual DbSet<BookCharacter> BooksCharacters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
