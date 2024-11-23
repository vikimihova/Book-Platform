using Microsoft.EntityFrameworkCore;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Core.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<Author, Guid> authorRepository;
        private readonly IRepository<Genre, Guid> genreRepository;

        public BookService(IRepository<Book, Guid> _bookRepository, IRepository<Author, Guid> _authorRepository, IRepository<Genre, Guid> _genreRepository)
        {
            bookRepository = _bookRepository;
            authorRepository = _authorRepository;
            genreRepository = _genreRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> allBooks = await bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => b.Author.LastName)
                .ThenBy(b => b.PublicationYear)
                .Select(b => new BookIndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    Author = b.Author.FullName,
                    Genre = b.Genre.Name,
                    ImageUrl = b.ImageUrl,
                    AverageRating = b.AverageRating,
                })
                .ToArrayAsync();

            return allBooks;
        }

        public async Task<BookDetailsViewModel?> GetBookDetailsAsync(string bookId)
        {
            // check if string is a valid Guid
            Guid parsedGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref parsedGuid))
            {
                return null;
            }

            // get book
            Book? book = await bookRepository
                .GetAllAttached()
                .Where(b => b.IsDeleted == false)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == parsedGuid);

            // check if book exists
            if (book == null)
            {
                return null;
            }

            // generate view model
            BookDetailsViewModel model = new BookDetailsViewModel()
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                Author = book.Author.FullName,
                Genre = book.Genre.Name,
                Description = book.Description,
                AverageRating = book.AverageRating,
                ImageUrl = book.ImageUrl
            };

            return model;
        }
    }
}
