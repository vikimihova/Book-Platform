using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        Task<BookDetailsViewModel?> GetBookDetailsAsync(string id);
    }
}
