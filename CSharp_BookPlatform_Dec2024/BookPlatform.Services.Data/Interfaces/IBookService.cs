using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Data.Models;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexViewModel>> IndexGetAllAsync();

        Task<IEnumerable<BookIndexViewModel>> IndexGetAllRandomAsync(BookIndexViewModelWrapper inputModel);        

        Task<BookDetailsViewModel> GetBookDetailsAsync(string id);

        Task<bool> AddBookAsync(AddBookInputModel model);

        Task<bool> EditBookAsync(EditBookInputModel model);

        Task<bool> SoftDeleteBookAsync(string bookId);

        Task<bool> IncludeBookAsync(string bookId);

        Task<EditBookInputModel> GenerateEditBookInputModelAsync(string bookId);
    }
}
