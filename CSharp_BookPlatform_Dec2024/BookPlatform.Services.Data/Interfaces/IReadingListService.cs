using BookPlatform.Data.Models;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.ApplicationUser;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IReadingListService
    {
        // MAIN
        Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId, ReadingListPaginatedViewModel inputModel);

        Task<ICollection<FriendBookViewModel>> GetFriendBooksByUserIdAsync(string userId);

        Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId);

        Task<bool> AddBookToUserReadingListReadAsync(ReadingListAddInputModel model, string userId);

        Task<bool> EditInReadingListAsync(ReadingListEditInputModel model, string userId);

        Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId);

        // AUXILIARY
        Task<ReadingStatus?> GetCurrentReadingStatusAsync(string bookId, string userId);

        Task<string?> GetCurrentReadingStatusDescriptionAsync(string bookId, string userId);

        Task<bool> CheckIfBookAlreadyReadAsync(Guid bookGuid, Guid userGuid, int readingStatusId);

        Task UpdateBookRating(string bookId);

        Task<ReadingListAddInputModel?> GenerateAddInputModelAsync(string bookId, string userId, int readingStatusId);

        Task<ReadingListEditInputModel?> GenerateEditInputModelAsync(string bookId, string userId, int readingStatusId);

        Task<int> GetTotalBooksCountPerUserAsync(string userId);
    }
}
