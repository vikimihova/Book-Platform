using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using BookPlatform.Services.Data.Interfaces;
using BookPlatform.Web.ViewModels.ReadingList;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> AddBookToUserReadingListAsync(string? bookId, string userId, int readingStatusId)
        {
            // check Guid bookId (if false, return false)
            Guid bookGuid = Guid.Empty;

            if (!this.IsGuidValid(bookId, ref bookGuid))
            {
                return false;
            }

            // check if book exists (if null, return false)
            Book? book = await this.bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                return false;
            }

            // parse userId to Guid
            Guid userGuid = Guid.Parse(userId);

            // check bookApplicationUser exists
            BookApplicationUser? bookApplicationUser = await this.bookApplicationUserRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(bau => bau.BookId == bookGuid && bau.ApplicationUserId == userGuid);

            // if false, create new bookApplicationUser
            // set reading status
            if (bookApplicationUser == null)
            {
                BookApplicationUser newBookApplicationUserToAdd = new BookApplicationUser()
                {
                    BookId = bookGuid,
                    ApplicationUserId = userGuid,
                    DateAdded = DateTime.Now,
                    ReadingStatusId = readingStatusId
                };

                // add to dbSet and save Changes
                await this.bookApplicationUserRepository.AddAsync(newBookApplicationUserToAdd);

                return true;
            }
            else
            {
                return false;
            }            
        }

        public Task<bool> RemoveBookFromUserReadingListAsync(string? bookId, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
