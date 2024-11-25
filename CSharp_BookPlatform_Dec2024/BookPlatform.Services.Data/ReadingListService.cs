using Microsoft.EntityFrameworkCore;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;

using static BookPlatform.Common.ApplicationConstants;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace BookPlatform.Core.Services
{
    public class ReadingListService : BaseService, IReadingListService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<Character, Guid> characterRepository;
        private readonly IRepository<Review, Guid> reviewRepository;
        private readonly IRepository<BookApplicationUser, object> bookApplicationUserRepository;

        public ReadingListService(
            UserManager<ApplicationUser> userManager,
            IRepository<Book, Guid> bookRepository,
            IRepository<Character, Guid> characterRepository,
            IRepository<Review, Guid> reviewRepository,
            IRepository<BookApplicationUser, object> bookApplicationUserRepository) : base(userManager)
        {
            this.userManager = userManager;
            this.bookRepository = bookRepository;
            this.characterRepository = characterRepository;
            this.reviewRepository = reviewRepository;
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

            if (bookApplicationUser != null && bookApplicationUser.ReadingStatusId != readingStatusId)
            {
                bookApplicationUser.ReadingStatusId = readingStatusId;
                await bookApplicationUserRepository.UpdateAsync(bookApplicationUser);

                return true;
            }
            
            return false;
        }

        public async Task<bool> AddBookToUserReadingListReadAsync(ReadingListInputModel model, string userId)
        {          
            // 1. ADD BOOK TO STANDARD READING LIST
            bool result = await AddBookToUserReadingListAsync(model.BookId, userId, model.ReadingStatus);

            // if unsuccessful, return false
            if (result == false) 
            {
                return false;
            }

            // parse userId to Guid
            Guid userGuid = Guid.Parse(userId);

            // check if bookApplicationUser exists after invoking add to reading list method
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(bau => bau.BookId.ToString().ToLower() == model.BookId.ToLower() && 
                                            bau.ApplicationUserId == userGuid);

            if (bookApplicationUser == null)
            {
                return false;
            }

            // 2. EXTEND INFORMATION ABOUT BookApplicationUser:

            // add rating
            bookApplicationUser.RatingId = model.Rating;
            await UpdateBookRating(model.BookId);

            // add date started
            if (model.DateStarted != null)
            {
                bool isStartDateValid = DateTime.TryParseExact(model.DateStarted, DateViewFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateStarted);

                if (isStartDateValid == true)
                {
                    bookApplicationUser.DateStarted = dateStarted;
                }
            }

            // add date finished
            if (model.DateFinished != null)
            {
                bool isFinishDateValid = DateTime.TryParseExact(model.DateFinished, DateViewFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished);

                if (isFinishDateValid == true)
                {
                    bookApplicationUser.DateFinished = dateFinished;
                }
            }

            // add favourite character
            if (model.CharacterId != null)
            {
                Guid characterGuid = Guid.Parse(model.CharacterId);
                bookApplicationUser.CharacterId = characterGuid;
                await bookApplicationUserRepository.UpdateAsync(bookApplicationUser);
            }

            // add review
            if (model.Review != null)
            {
                Review? review = await this.reviewRepository
                    .GetAllAttached()
                    .FirstOrDefaultAsync(r => r.BookId == bookApplicationUser.BookId &&
                                              r.ApplicationUserId == bookApplicationUser.ApplicationUserId);

                if (review == null)
                {
                    review = new Review()
                    {
                        Content = model.Review,
                        BookId = bookApplicationUser.BookId,
                        ApplicationUserId = bookApplicationUser.ApplicationUserId
                    };

                    await this.reviewRepository.AddAsync(review);
                }                
            }           

            return true;
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

        public async Task<bool> CheckIfBookAlreadyReadAsync(string bookId, string userId, int readingStatusId)
        {
            // CHECK IF BookApplicationUser WITH STATUS READ ALREADY EXISTS
            // parse userId to Guid
            Guid userGuid = Guid.Parse(userId);

            // check bookApplicationUser exists
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(bau => bau.BookId.ToString().ToLower() == bookId.ToLower() 
                                         && bau.ApplicationUserId == userGuid);

            if (bookApplicationUser != null && bookApplicationUser.ReadingStatusId == 1)
            {
                return true;
            }

            return false;
        }

        public async Task UpdateBookRating(string bookId)
        {
            Guid bookGuid = Guid.Parse(bookId);

            Book? book = this.bookRepository.GetById(bookGuid);

            if (book != null)
            {
                book.AverageRating = this.bookApplicationUserRepository
                    .GetAllAttached()
                    .Where(bau => bau.RatingId != null)
                    .Average(bau => bau.RatingId!.Value);

                await this.bookRepository.UpdateAsync(book);
            }
        }
    }
}
