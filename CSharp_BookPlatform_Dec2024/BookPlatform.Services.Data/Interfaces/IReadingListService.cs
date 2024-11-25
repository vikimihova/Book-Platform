using BookPlatform.Data.Models;
using BookPlatform.Core.ViewModels.ReadingList;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IReadingListService
    {
        Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId);

        Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId);

        Task<bool> AddBookToUserReadingListReadAsync(ReadingListInputModel model, string userId);

        Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId);

        Task<ReadingStatus?> GetReadingStatusForCurrentBookApplicationUserAsync(string bookId, Guid userGuid);

        Task<bool> CheckIfBookAlreadyReadAsync(string bookId, string userId, int readingStatusId);

        Task UpdateBookRating(string bookId);
    }
}
