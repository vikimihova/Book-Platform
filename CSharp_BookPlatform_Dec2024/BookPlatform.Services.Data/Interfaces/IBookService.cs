using BookPlatform.Web.ViewModels.Book;

namespace BookPlatform.Services.Data.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        Task<BookDetailsViewModel?> GetBookDetailsAsync(string id);
    }
}
