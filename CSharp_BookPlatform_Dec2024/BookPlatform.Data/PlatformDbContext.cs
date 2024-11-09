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

        public void SeedAuthors()
        {
            try
            {
                AuthorImportDto[] authorImportDtos = Deserializer.GenerateAuthorImportDtos();

                List<Author> generatedAuthors = new List<Author>();

                foreach (var authorDto in authorImportDtos)
                {
                    if (!generatedAuthors.Any(a => a.FullName == authorDto.Author))
                    {
                        Author author = new Author()
                        {
                            FullName = authorDto.Author,
                        };

                        generatedAuthors.Add(author);
                    }
                }

                List<Author> existingAuthors = this.Authors
                    .AsNoTracking()
                    .ToList();

                List<Author> newAuthorsToAdd = generatedAuthors
                    .Where(ga => !existingAuthors.Any(ea => ea.FullName == ga.FullName))
                    .ToList();

                if (newAuthorsToAdd.Any())
                {
                    this.Authors.AddRange(newAuthorsToAdd);
                    this.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SeedGenres()
        {
            try
            {
                GenreImportDto[] genreImportDtos = Deserializer.GenerateGenreImportDtos();

                List<Genre> generatedGenres = new List<Genre>();

                foreach (var genreDto in genreImportDtos)
                {
                    if (!generatedGenres.Any(g => g.Name == genreDto.Genre))
                    {
                        Genre genre = new Genre()
                        {
                            Name = genreDto.Genre,
                        };

                        generatedGenres.Add(genre);
                    }
                }

                List<Genre> existingGenres = this.Genres
                    .AsNoTracking()
                    .ToList();

                List<Genre> newGenresToAdd = generatedGenres
                    .Where(gg => !existingGenres.Any(eg => eg.Name == gg.Name))
                    .ToList();

                if (newGenresToAdd.Any())
                {
                    this.Genres.AddRange(newGenresToAdd);
                    this.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
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
                            .AsNoTracking()
                            .FirstOrDefault(a => a.FullName == bookDto.Author);

                        if (author == null)
                        {
                            continue;
                        }

                        Genre? genre = this.Genres
                            .AsNoTracking()
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

                List<Book> existingBooks = this.Books
                    .AsNoTracking()
                    .ToList();

                List<Book> newBooksToAdd = generatedBooks
                    .Where(gb => !existingBooks.Any(eb => eb.Title == gb.Title && eb.AuthorId == gb.AuthorId))
                    .ToList();

                if (newBooksToAdd.Any())
                {
                    this.Books.AddRange(newBooksToAdd);
                    this.SaveChanges();
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine($"An error occurred: {ex.Message}"); 
            }            
        }

        public void SeedCharacters()
        {
            try
            {
                CharactersImportDto[] charactersImportDtos = Deserializer.GenerateCharactersImportDtos();

                List<Character> generatedCharacters = new List<Character>();

                foreach (var charactersDto in charactersImportDtos)
                {
                    if (charactersDto.Characters.Count == 0)
                    {
                        continue;
                    }

                    foreach (var characterName in charactersDto.Characters)
                    {
                        if (!generatedCharacters.Any(c => c.Name == characterName))
                        {
                            Character character = new Character()
                            {
                                Name = characterName,
                            };

                            generatedCharacters.Add(character);
                        }
                    }
                }

                List<Character> existingCharacters = this.Characters
                    .AsNoTracking()
                    .ToList();

                List<Character> newCharactersToAdd = generatedCharacters
                    .Where(gc => !existingCharacters.Any(ec => ec.Name == gc.Name))
                    .ToList();

                if (newCharactersToAdd.Any())
                {
                    this.Characters.AddRange(newCharactersToAdd);
                    this.SaveChanges();
                }

                // Continue with seeding the corresponding BookCharacters
                this.SeedBookCharacters();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void SeedBookCharacters()
        {
            try
            {
                CharactersImportDto[] bookImportDtos = Deserializer.GenerateCharactersImportDtos();

                List<BookCharacter> generatedBookCharacters = new List<BookCharacter>();

                foreach (var bookDto in bookImportDtos)
                {
                    if (bookDto.Characters.Count == 0)
                    {
                        continue;
                    }

                    Book? book = this.Books
                        .AsNoTracking()
                        .FirstOrDefault(b => b.Title == bookDto.Title && b.Description == bookDto.Description);

                    if (book == null)
                    {
                        continue;
                    }

                    foreach (var characterName in bookDto.Characters)
                    {
                        Character? character = this.Characters
                            .AsNoTracking()
                            .FirstOrDefault(c => c.Name == characterName);

                        if (character == null)
                        {
                            continue;
                        }

                        BookCharacter bookCharacter = new BookCharacter()
                        {
                            BookId = book.Id,
                            CharacterId = character.Id,
                        };

                        generatedBookCharacters.Add(bookCharacter);
                    }

                }

                List<BookCharacter> existingBookCharacters = this.BooksCharacters
                    .AsNoTracking()
                    .ToList();

                List<BookCharacter> newBookCharactersToAdd = generatedBookCharacters
                    .Where(gbc => !existingBookCharacters.Any(ebc => ebc.BookId == gbc.BookId && ebc.CharacterId == gbc.CharacterId))
                    .ToList();

                if (newBookCharactersToAdd.Any())
                {
                    this.BooksCharacters.AddRange(newBookCharactersToAdd);
                    this.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void UpdateBooksImageUrl()
        {
            //BookImportDto[] bookImportDtos = Deserializer.GenerateBookImportDtos();

            //foreach (var bookDto in bookImportDtos)
            //{
            //    Book? book = this.Books
            //            .FirstOrDefault(b => b.Title == bookDto.Title && b.Description == bookDto.Description);

            //    if (book == null)
            //    {
            //        continue;
            //    }

            //    book.ImageUrl = "/" + bookDto.ImageLink;
            //    this.SaveChanges();
            //}
        }
    }
}
