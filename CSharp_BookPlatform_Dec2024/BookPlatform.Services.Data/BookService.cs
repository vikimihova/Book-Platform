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

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllRandomAsync()
        {
            Random random = new Random();

            IEnumerable<BookIndexViewModel> allBooks = await IndexGetAllAsync();

            List<BookIndexViewModel> allBooksRandom = allBooks
                .Where(b => b.IsDeleted == false)
                .OrderBy(b => random.Next()).ToList();

            return allBooksRandom;
        }

        public Task<IEnumerable<BookIndexViewModel>> GetBooksPerGenreAsync(string genreId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookIndexViewModel>> GetBooksPerAuthorAsync(string authorId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookIndexViewModel>> GetBooksOrderedByYearAscendingAsync(IEnumerable<BookIndexViewModel> model)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BookIndexViewModel>> GetBooksOrderedByYearDescendingAsync(IEnumerable<BookIndexViewModel> model)
        {
            throw new NotImplementedException();
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

        public async Task<Book?> GetBookByIdAsync(string bookId)
        {
            Book? book = await this.bookRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(b => b.Id.ToString().ToLower() == bookId.ToLower());

            return book;
        }

        //public async Task<ICollection<SelectBookViewModel>> GetBooksAsync()
        //{
        //    ICollection<SelectBookViewModel> books = await this.bookRepository
        //        .GetAllAttached()
        //        .Where(b => b.IsDeleted == false)
        //        .Select(b => new SelectBookViewModel()
        //        {
        //            Id = b.Id.ToString(),
        //            Title = b.Title
        //        })
        //        .ToListAsync();

        //    return books;
        //}

        public async Task<bool> AddBookAsync(AddBookInputModel model)
        {
            // check if book already exists
            Book? book = await this.bookRepository
                .FirstOrDefaultAsync(b => b.Title == model.Title 
                                       && b.AuthorId.ToString().ToLower() == model.AuthorId);

            if (book != null)
            {
                return false;
            }

            // check if guids are valid
            Guid authorGuid = Guid.Empty;
            Guid genreGuid = Guid.Empty;

            if (!IsGuidValid(model.AuthorId, ref authorGuid) || !IsGuidValid(model.GenreId, ref genreGuid))
            {
                return false;                
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
            // check if book already exists
            Book? book = await this.bookRepository
                .FirstOrDefaultAsync(b => b.Title == model.Title
                                       && b.AuthorId.ToString().ToLower() == model.AuthorId);

            if (book == null)
            {
                return false;
            }

            // check if guids are valid
            Guid authorGuid = Guid.Empty;
            Guid genreGuid = Guid.Empty;

            if (!IsGuidValid(model.AuthorId, ref authorGuid) || !IsGuidValid(model.GenreId, ref genreGuid))
            {
                return false;
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
            // check if bookId is valid guid
            Guid bookGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref bookGuid))
            {
                return false;
            }

            // check if book exists
            Book? book = await this.bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                return false;
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
            // check if bookId is valid guid
            Guid bookGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref bookGuid))
            {
                return false;
            }

            // check if book exists
            Book? book = await this.bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                return false;
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

        public async Task<EditBookInputModel?> GenerateEditBookInputModelAsync(string bookId)
        {
            // check if bookId is valid guid
            Guid bookGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref bookGuid))
            {
                return null;
            }

            // check if book exists
            Book? book = await this.bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                return null;
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
