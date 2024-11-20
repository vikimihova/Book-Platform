using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using BookPlatform.Services.Data.Interfaces;
using BookPlatform.Web.ViewModels.ReadingList;

namespace BookPlatform.Services.Data
{
    public class ReadingListService : BaseService, IReadingListService
    {
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<BookApplicationUser, object> bookApplicationUserRepository;

        public ReadingListService(
            IRepository<Book, Guid> bookRepository,
            IRepository<BookApplicationUser, object> bookApplicationUserRepository)
        {
            this.bookRepository = bookRepository;
            this.bookApplicationUserRepository = bookApplicationUserRepository;
        }

        public Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddBookToUserReadingListAsync(string? bookId, string userId, int readingStatusId)
        {
            // check Guid bookId (if false, return false)
            // check if book exists (if null, return false)
            // check bookApplicationUser exists
            // if false, create new bookApplicationUser
            // set reading status
            // add to dbSet and save Changes
            // return true

            throw new NotImplementedException();
        }

        public Task<bool> RemoveBookFromUserReadingListAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
