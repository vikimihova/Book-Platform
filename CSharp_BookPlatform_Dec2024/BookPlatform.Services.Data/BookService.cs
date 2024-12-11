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

        public BookService(IRepository<Book, Guid> _bookRepository)
        {
            bookRepository = _bookRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> allBooks = await bookRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .OrderBy(b => b.Author.LastName)
                .ThenBy(b => b.PublicationYear)
                .Select(b => new BookIndexViewModel()
                {
                    Id = b.Id.ToString(),
                    Title = b.Title,
                    Author = b.Author.FullName,
                    AuthorLastName = b.Author.LastName != null ? b.Author.LastName : "-",
                    AuthorFirstName = b.Author.FirstName != null ? b.Author.FirstName : "-",
                    Genre = b.Genre.Name,
                    ImageUrl = b.ImageUrl,
                    AverageRating = b.AverageRating,
                    IsDeleted = b.IsDeleted
                })
                .ToListAsync();

            return allBooks;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllRandomAsync(BookIndexViewModelWrapper model)
        {
            Random random = new Random();

            IEnumerable<BookIndexViewModel> allBooks = await IndexGetAllAsync();

            if (!String.IsNullOrWhiteSpace(model.SearchInput))
            {
                if (allBooks.Any(b => b.Title.ToLower().Contains(model.SearchInput.ToLower())))
                {
                    allBooks = allBooks
                        .Where(b => b.Title.ToLower().Contains(model.SearchInput.ToLower()))
                        .ToList();
                }                   

                if (allBooks.Any(b => b.Author.ToLower().Contains(model.SearchInput.ToLower())))
                {
                    allBooks = allBooks
                        .Where(b => b.Author.ToLower().Contains(model.SearchInput.ToLower()))
                        .ToList();
                }
            }

            if (!String.IsNullOrWhiteSpace(model.GenreFilter))
            {
                allBooks = allBooks
                    .Where(b => b.Genre == model.GenreFilter)
                    .ToList();
            }

            List<BookIndexViewModel> allBooksRandom = allBooks
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => random.Next()).ToList();

            return allBooksRandom;
        }        

        public async Task<BookDetailsViewModel> GetBookDetailsAsync(string bookId)
        {           
            // check if string is a valid Guid
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // get book
            Book? book = await bookRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(b => b.IsDeleted == false)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == bookGuid);

            // check if book exists
            if (book == null)
            {
                throw new InvalidOperationException();
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
                 
        public async Task<bool> AddBookAsync(AddBookInputModel model)
        {
            // check if guids are valid
            Guid authorGuid = Guid.Empty;
            Guid genreGuid = Guid.Empty;
            if (!IsGuidValid(model.AuthorId, ref authorGuid) || !IsGuidValid(model.GenreId, ref genreGuid))
            {
                throw new ArgumentException();
            }

            // check if book already exists
            Book? book = await this.bookRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title == model.Title 
                                       && b.AuthorId == authorGuid);

            if (book != null)
            {
                throw new InvalidOperationException();
            }            

            book = new Book()
            {
                Title = model.Title,
                PublicationYear = model.PublicationYear,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                AuthorId = authorGuid,
                GenreId = genreGuid,
            };

            await this.bookRepository.AddAsync(book);

            return true;
        }

        public async Task<bool> EditBookAsync(EditBookInputModel model)
        {
            // check if guids are valid
            Guid authorGuid = Guid.Empty;
            Guid genreGuid = Guid.Empty;
            if (!IsGuidValid(model.AuthorId, ref authorGuid) || !IsGuidValid(model.GenreId, ref genreGuid))
            {
                throw new ArgumentException();
            }

            // check if book already exists
            Book? book = await this.bookRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.Title == model.Title
                                       && b.AuthorId == authorGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }            

            book.Title = model.Title;
            book.PublicationYear = model.PublicationYear;
            book.Description = model.Description;
            book.ImageUrl = model.ImageUrl;
            book.AuthorId = authorGuid;
            book.GenreId = genreGuid;

            await this.bookRepository.UpdateAsync(book);

            return true;
        }

        public async Task<bool> SoftDeleteBookAsync(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.FirstOrDefaultAsync(b => b.Id == bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            // check if book already deleted
            if (book.IsDeleted == true)
            {
                return false;
            }

            // soft delete book
            book.IsDeleted = true;
            await this.bookRepository.UpdateAsync(book);

            return true;
        }

        public async Task<bool> IncludeBookAsync(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.FirstOrDefaultAsync(b => b.Id == bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            // check if book already deleted
            if (book.IsDeleted != true)
            {
                return false;
            }

            // include book
            book.IsDeleted = false;
            await this.bookRepository.UpdateAsync(book);

            return true;
        }

        // AUXILIARY

        public async Task<EditBookInputModel> GenerateEditBookInputModelAsync(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            EditBookInputModel model = new EditBookInputModel()
            {
                Title = book.Title,
                PublicationYear = book.PublicationYear,
                Description = book.Description,
                ImageUrl = book.ImageUrl,
                AuthorId = book.AuthorId.ToString(),
                GenreId = book.GenreId.ToString(),
            };

            return model;
        }
    }
}
