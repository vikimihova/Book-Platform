using BookPlatform.Data.Models;
using BookPlatform.Services.Data.DataProcessor;
using BookPlatform.Services.Data.DataProcessor.ImportDtos;
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

        public void SeedBooks()
        {
            try
            {
                BookImportDto[] bookImportDtos = Deserializer.GenerateBookImportDtos();

                List<Book> generatedBooks = new List<Book>();

                foreach (var bookDto in bookImportDtos)
                {
                    if (!generatedBooks.Any(b => b.Title == bookDto.Title && b.Description == bookDto.Description))
                    {
                        Author? author = this.Authors
                            .FirstOrDefault(a => a.FullName == bookDto.Author);

                        if (author == null)
                        {
                            continue;
                        }

                        Genre? genre = this.Genres
                            .FirstOrDefault(g => g.Name == bookDto.Genre);

                        if (genre == null)
                        {
                            continue;
                        }

                        Book book = new Book()
                        {
                            Title = bookDto.Title,
                            PublicationYear = bookDto.Year,
                            AuthorId = author.Id,
                            GenreId = genre.Id,
                            Description = bookDto.Description,
                            ImageUrl = bookDto.ImageLink,
                        };


                        generatedBooks.Add(book);
                    }
                }

                List<Book> existingBooks = this.Books.ToList();
                List<Book> booksToAdd = generatedBooks
                    .Where(gb => !existingBooks.Any(eb => eb.Title == gb.Title && eb.AuthorId == gb.AuthorId))
                    .ToList();

                if (booksToAdd.Any())
                {
                    this.Books.AddRange(booksToAdd);
                    this.SaveChanges();
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"An error occurred: {ex.Message}"); 
            }            
        }
    }
}
