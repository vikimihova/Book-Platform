using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Data.Models;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        Task<IEnumerable<BookIndexViewModel>> GetBooksPerGenreAsync(string genreId);

        Task<IEnumerable<BookIndexViewModel>> GetBooksPerAuthorAsync(string authorId);

        Task<IEnumerable<BookIndexViewModel>> GetBooksOrderedByYearAscendingAsync(IEnumerable<BookIndexViewModel> model);

        Task<IEnumerable<BookIndexViewModel>> GetBooksOrderedByYearDescendingAsync(IEnumerable<BookIndexViewModel> model);

        Task<BookDetailsViewModel?> GetBookDetailsAsync(string id);

        Task<Book?> GetBookByIdAsync(string bookId);

        Task<ICollection<SelectBookViewModel>> GetBooksAsync();

        Task<bool> AddBookAsync(AddBookInputModel model);

        //Task<BookDetailsViewModel> UpdateBookAsync();

        //Task<BookDetailsViewModel> DeleteBookAsync();
    }
}
