using BookPlatform.Web.ViewModels.ReadingList;

namespace BookPlatform.Services.Data.Interfaces
{
    public interface IReadingListService
    {
        Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId);

        Task<bool> AddBookToUserReadingListAsync(string? bookId, string userId, int readingStatusId);

        Task<bool> RemoveBookFromUserReadingListAsync(string? bookId, string userId);
    }
}
