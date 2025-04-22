using BookPlatform.Data.Models;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.ApplicationUser;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IReadingListService
    {
        // MAIN
        Task<IEnumerable<ListedBookViewModel>> GetUserReadingListByUserIdAsync(string userId, ReadingListPaginatedViewModel inputModel);

        Task<ICollection<FriendBookViewModel>> GetFriendBooksByUserIdAsync(string userId);

        Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId);

        Task<bool> AddBookToUserReadingListReadAsync(AddListedBookInputModel model, string userId);

        Task<bool> EditInReadingListAsync(EditListedBookInputModel model, string userId);

        Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId);

        // AUXILIARY
        Task<ReadingStatus?> GetCurrentReadingStatusAsync(string bookId, string userId);

        Task<string?> GetCurrentReadingStatusDescriptionAsync(string bookId, string userId);

        Task<bool> CheckIfBookAlreadyReadAsync(Guid bookGuid, Guid userGuid, int readingStatusId);

        Task UpdateBookRating(string bookId);

        Task<AddListedBookInputModel?> GenerateAddInputModelAsync(string bookId, string userId, int readingStatusId);

        Task<EditListedBookInputModel?> GenerateEditInputModelAsync(string bookId, string userId, int readingStatusId);

        Task<int> GetTotalBooksCountPerUserAsync(string userId);
    }
}
