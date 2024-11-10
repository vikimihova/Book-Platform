using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using BookPlatform.Services.Data.Interfaces;
using BookPlatform.Web.ViewModels.Book;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Services.Data
{
    public class BookService : BaseService, IBookService
    {
        private readonly IRepository<Book, Guid> bookRepository;

        public BookService(IRepository<Book, Guid> _bookRepository)
        {
            this.bookRepository = _bookRepository;
        }

        public async Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync()
        {
            IEnumerable<BookIndexViewModel> allBooks = await this.bookRepository
                .GetAllAttached()
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
    }
}
