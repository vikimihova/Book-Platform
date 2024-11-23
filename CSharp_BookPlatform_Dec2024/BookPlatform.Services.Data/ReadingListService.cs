using Microsoft.EntityFrameworkCore;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;

using static BookPlatform.Common.ApplicationConstants;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Core.Services
{
    public class ReadingListService : BaseService, IReadingListService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<BookApplicationUser, object> bookApplicationUserRepository;

        public ReadingListService(
            UserManager<ApplicationUser> userManager,
            IRepository<Book, Guid> bookRepository,
            IRepository<BookApplicationUser, object> bookApplicationUserRepository) : base(userManager)
        {
            this.userManager = userManager;
            this.bookRepository = bookRepository;
            this.bookApplicationUserRepository = bookApplicationUserRepository;
        }

        public async Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId)
        {
            // get UserBooks and create view model

            IEnumerable<ReadingListViewModel> readingList = await bookApplicationUserRepository
                .GetAllAttached()
                .Where(bau => bau.ApplicationUserId.ToString() == userId)
                .Include(bau => bau.ReadingStatus)
                .Include(bau => bau.Rating)
                .Include(bau => bau.Book)
                .ThenInclude(b => b.Author)
                .Select(bau => new ReadingListViewModel()
                {
                    BookId = bau.BookId.ToString(),
                    BookTitle = bau.Book.Title,
                    Author = bau.Book.Author.FullName,
                    Rating = bau.RatingId != null ? bau.RatingId.Value : 0,
                    ReadingStatus = bau.ReadingStatus.StatusDescription,
                    DateAdded = bau.DateAdded.ToString(DateViewFormat),
                    DateStarted = bau.DateStarted.HasValue ? bau.DateStarted.Value.ToString(DateViewFormat) : String.Empty,
                    DateFinished = bau.DateFinished.HasValue ? bau.DateFinished.Value.ToString(DateViewFormat) : String.Empty,
                    ImageUrl = bau.Book.ImageUrl
                })
                .ToListAsync();

            return readingList;
        }

        public async Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId)
        {
            // check Guid bookId (if false, return false)
            Guid bookGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref bookGuid))
            {
                return false;
            }

            // check if book exists (if null, return false)
            Book? book = await bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                return false;
            }

            // parse userId to Guid
            Guid userGuid = Guid.Parse(userId);

            // check bookApplicationUser exists
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
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
                await bookApplicationUserRepository.AddAsync(newBookApplicationUserToAdd);

                return true;
            }
            else
            {
                return false;
            }
        }

        public Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ReadingStatus?> GetReadingStatusForCurrentBookApplicationUserAsync(string bookId, Guid userGuid)
        {
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                                .GetAllAttached()
                                .Include(x => x.ReadingStatus)
                                .FirstOrDefaultAsync(x => x.BookId.ToString() == bookId && x.ApplicationUserId == userGuid);

            ReadingStatus? readingStatus = bookApplicationUser?.ReadingStatus;

            return readingStatus;
        }
    }
}
