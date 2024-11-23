using BookPlatform.Data.Models;
using BookPlatform.Core.ViewModels.ReadingList;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IReadingListService
    {
        Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId);

        Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId);

        Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId);

        Task<ReadingStatus?> GetReadingStatusForCurrentBookApplicationUserAsync(string bookId, Guid userGuid);
    }
}
