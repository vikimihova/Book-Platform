using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.ApplicationUser;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Core.Services
{
    public class ReadingListService : BaseService, IReadingListService
    {
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<Review, Guid> reviewRepository;
        private readonly IRepository<BookApplicationUser, object> bookApplicationUserRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReadingListService(
            IRepository<Book, Guid> bookRepository,
            IRepository<Review, Guid> reviewRepository,
            IRepository<BookApplicationUser, object> bookApplicationUserRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.bookRepository = bookRepository;
            this.reviewRepository = reviewRepository;
            this.bookApplicationUserRepository = bookApplicationUserRepository;
            this.userManager = userManager;
        }

        // MAIN

        public async Task<IEnumerable<ReadingListViewModel>> GetUserReadingListByUserIdAsync(string userId, ReadingListPaginatedViewModel inputModel)
        {
            IEnumerable<ReadingListViewModel> readingList = new List<ReadingListViewModel>();

            // check if userId is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // find user
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // get UserBooks and create view model
            IQueryable<ReadingListViewModel> readingListQuery = bookApplicationUserRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(bau => bau.ApplicationUserId == userGuid)
                .Include(bau => bau.ReadingStatus)
                .Include(bau => bau.Rating)
                .Include(bau => bau.Character)
                .Include(bau => bau.Book)
                .ThenInclude(b => b.Author)
                .OrderByDescending(b => b.DateAdded)
                .Select(bau => new ReadingListViewModel()
                {
                    BookId = bau.BookId.ToString(),
                    BookTitle = bau.Book.Title,
                    Author = bau.Book.Author.FullName,
                    Rating = bau.RatingId != null ? bau.RatingId.Value : 0,
                    ReadingStatusId = bau.ReadingStatus.Id,
                    ReadingStatus = bau.ReadingStatus.StatusDescription,
                    DateAdded = bau.DateAdded.ToString(DateViewFormat),
                    DateFinished = bau.DateFinished.HasValue ? bau.DateFinished.Value.ToString(DateViewFormat) : String.Empty,
                    FavoriteCharacter = bau.Character != null ? bau.Character.Name : null,
                    ImageUrl = bau.Book.ImageUrl
                });      

            if (inputModel.CurrentPage.HasValue &&
                inputModel.EntitiesPerPage.HasValue)
            {
                readingListQuery = readingListQuery
                    .Skip(inputModel.EntitiesPerPage.Value * (inputModel.CurrentPage.Value - 1))
                    .Take(inputModel.EntitiesPerPage.Value);
            }

            readingList = await readingListQuery.ToListAsync();

            return readingList;
        }

        public async Task<ICollection<FriendBookViewModel>> GetFriendBooksByUserIdAsync(string userId)
        {
            ICollection<FriendBookViewModel> model = new List<FriendBookViewModel>();

            // check if user id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // find user
            ApplicationUser? user = await this.userManager.Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if user has any friends
            if (!user.Friends.Any())
            {
                return model;
            }            

            foreach(var friend in user.Friends)
            {
                List<BookApplicationUser> friendsReadingList = await bookApplicationUserRepository
                    .GetAllAttached()
                    .AsNoTracking()
                    .Where(bau => bau.ApplicationUserId == friend.Id)
                    .Include(bau => bau.ReadingStatus)
                    .Include(bau => bau.Book)
                    .ToListAsync();

                if (!friendsReadingList.Any())
                {
                    continue;
                }

                foreach (var bau in friendsReadingList)
                {
                    FriendBookViewModel viewModel = new FriendBookViewModel()
                    {
                        FriendUserName = friend.UserName!,
                        BookId = bau.BookId.ToString(),
                        Title = bau.Book.Title,
                        ReadingStatusDescription = bau.ReadingStatus.StatusDescription,
                        DateAdded = bau.DateAdded,
                        ImageUrl = bau.Book.ImageUrl
                    };

                    model.Add(viewModel);
                }                
            }

            ICollection<FriendBookViewModel> orderedModel = model
                .OrderByDescending(fbvm => fbvm.DateAdded)
                .ToList();

            return orderedModel;
        }

        public async Task<bool> AddBookToUserReadingListAsync(string bookId, string userId, int readingStatusId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) ||
                !IsGuidValid(userId, ref userGuid) ||
                readingStatusId < 1 ||
                readingStatusId > 3)
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await bookRepository
                .GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }                       

            // check if book already read
            bool IsAlreadyRead = await CheckIfBookAlreadyReadAsync(bookGuid, userGuid, readingStatusId);

            if (IsAlreadyRead)
            {
                return false;
            }

            // check bookApplicationUser exists
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
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

        public async Task<bool> AddBookToUserReadingListReadAsync(ReadingListAddInputModel model, string userId)
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

            // add date finished
            if (model.DateFinished != null)
            {
                bool isFinishDateValid = DateTime.TryParseExact(model.DateFinished, DateInputFormat,
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

            // update repository
            await bookApplicationUserRepository.UpdateAsync(bookApplicationUser);

            // update book rating
            await UpdateBookRating(model.BookId);

            return true;
        }

        public async Task<bool> EditInReadingListAsync(ReadingListEditInputModel model, string userId)
        {
            // parse userId to Guid
            Guid userGuid = Guid.Empty;
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid) ||
                !IsGuidValid(model.BookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if bookApplicationUser exists
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                .FirstOrDefaultAsync(bau => bau.BookId == bookGuid &&
                                            bau.ApplicationUserId == userGuid);

            if (bookApplicationUser == null)
            {
                throw new InvalidOperationException();
            }

            // UPDATE INFORMATION ABOUT BookApplicationUser:

            // update rating
            bookApplicationUser.RatingId = model.Rating;                       

            // update date finished
            if (model.DateFinished != null)
            {
                bool isFinishDateValid = DateTime.TryParseExact(model.DateFinished, DateInputFormat,
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished);

                if (isFinishDateValid == true)
                {
                    bookApplicationUser.DateFinished = dateFinished;
                }
            }

            // update favourite character
            if (model.CharacterId != null)
            {
                Guid characterGuid = Guid.Empty;
                if (!IsGuidValid(model.CharacterId, ref characterGuid))
                {
                    throw new ArgumentException();
                }

                bookApplicationUser.CharacterId = characterGuid;
            }
            else
            {
                bookApplicationUser.CharacterId = null;
            }

            // update review
            Review? review = await this.reviewRepository
                    .FirstOrDefaultAsync(r => r.BookId == bookApplicationUser.BookId &&
                                              r.ApplicationUserId == bookApplicationUser.ApplicationUserId);

            if (model.Review != null && review != null && model.Review != review.Content)
            {
                review.Content = model.Review;
                review.ModifiedOn = DateTime.Now;
                await this.reviewRepository.UpdateAsync(review);
            }

            if (model.Review == null && review != null)
            {
                await this.reviewRepository.DeleteAsync(review);
            }

            if (model.Review != null && review == null)
            {
                review = new Review()
                {
                    Content = model.Review,
                    BookId = bookApplicationUser.BookId,
                    ApplicationUserId = bookApplicationUser.ApplicationUserId
                };

                await this.reviewRepository.AddAsync(review);
            }            

            // update repository
            await bookApplicationUserRepository.UpdateAsync(bookApplicationUser);

            // update book rating
            await UpdateBookRating(model.BookId);

            return true;
        }

        public async Task<bool> RemoveBookFromUserReadingListAsync(string bookId, string userId)
        {
            // try parse string to guid
            Guid bookGuid = Guid.Empty;
            Guid userGuid = Guid.Empty;

            if (!IsGuidValid(bookId, ref bookGuid) || 
                !IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // get entry to delete
            BookApplicationUser? bookApplicationUserToDelete = await this.bookApplicationUserRepository
                .FirstOrDefaultAsync(bau => bau.BookId == bookGuid && 
                                            bau.ApplicationUserId == userGuid);

            // delete entry
            if (bookApplicationUserToDelete != null) 
            {
                await this.bookApplicationUserRepository
                .DeleteAsync(bookApplicationUserToDelete);


                // update average rating
                await UpdateBookRating(bookId);

                return true;
            }

            return false;
        }

        // AUXILIARY

        public async Task<ReadingStatus?> GetCurrentReadingStatusAsync(string bookId, string userId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) ||
                !IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                                .GetAllAttached()
                                .AsNoTracking()
                                .Include(x => x.ReadingStatus)
                                .FirstOrDefaultAsync(x => x.BookId == bookGuid &&
                                                          x.ApplicationUserId == userGuid);

            if (bookApplicationUser == null)
            {
                return null;
            }

            ReadingStatus readingStatus = bookApplicationUser.ReadingStatus;

            return readingStatus;
        }

        public async Task<string?> GetCurrentReadingStatusDescriptionAsync(string bookId, string userId)
        {
            ReadingStatus? readingStatus = await GetCurrentReadingStatusAsync(bookId, userId);

            if (readingStatus != null)
            {
                return readingStatus.StatusDescription;
            }

            return null;
        }

        public async Task<bool> CheckIfBookAlreadyReadAsync(Guid bookGuid, Guid userGuid, int readingStatusId)
        {
            // CHECK IF BookApplicationUser WITH STATUS READ ALREADY EXISTS
            
            BookApplicationUser? bookApplicationUser = await bookApplicationUserRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(bau => bau.BookId == bookGuid &&
                                            bau.ApplicationUserId == userGuid);

            if (bookApplicationUser != null && bookApplicationUser.ReadingStatusId == 1)
            {
                return true;
            }

            return false;
        }

        public async Task UpdateBookRating(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.FirstOrDefaultAsync(b => b.Id == bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            List<BookApplicationUser> relevantBookApplicationUsersWithRating = await this.bookApplicationUserRepository
                        .GetAllAttached()
                        .AsNoTracking()
                        .Where(bau => bau.BookId == bookGuid && bau.RatingId != null)
                        .ToListAsync();

            if (relevantBookApplicationUsersWithRating.Any())
            {
                book.AverageRating = relevantBookApplicationUsersWithRating.Average(bau => bau.RatingId!.Value);
            }
            else
            {
                book.AverageRating = 0;
            }

            await this.bookRepository.UpdateAsync(book);
        }

        public async Task<ReadingListAddInputModel?> GenerateAddInputModelAsync(string bookId, string userId, int readingStatusId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) || 
                !IsGuidValid(userId, ref userGuid) ||
                readingStatusId < 1 ||
                readingStatusId > 3)
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            // check if book already read
            bool IsAlreadyRead = await CheckIfBookAlreadyReadAsync(bookGuid, userGuid, readingStatusId);

            if (IsAlreadyRead)
            {
                return null;
            }

            ReadingListAddInputModel model = new ReadingListAddInputModel();

            model.BookId = book.Id.ToString();
            model.BookTitle = book.Title;
            model.ReadingStatus = readingStatusId;
            model.ImageUrl = book.ImageUrl;

            return model;
        }

        public async Task<ReadingListEditInputModel?> GenerateEditInputModelAsync(string bookId, string userId, int readingStatusId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) ||
                !IsGuidValid(userId, ref userGuid) ||
                readingStatusId < 1 ||
                readingStatusId > 3)
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            // check if book already read
            bool IsAlreadyRead = await CheckIfBookAlreadyReadAsync(bookGuid, userGuid, readingStatusId);

            if (!IsAlreadyRead)
            {
                return null;
            }

            // get bookApplicationUser
            BookApplicationUser? bookApplicationUser = await this.bookApplicationUserRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(bau => bau.BookId == bookGuid
                                         && bau.ApplicationUserId == userGuid);            

            // generate model
            ReadingListEditInputModel model = new ReadingListEditInputModel()
            {
                BookId = bookId,
                BookTitle = book.Title,
                Rating = bookApplicationUser.RatingId != null ? bookApplicationUser.RatingId : null,
                ReadingStatus = 3,
                DateFinished = bookApplicationUser.DateFinished.HasValue ? bookApplicationUser.DateFinished.Value.ToString(DateInputFormat) : String.Empty,
                CharacterId = bookApplicationUser.CharacterId.ToString(),
                ImageUrl = book.ImageUrl
            };

            // check review content
            Review? review = await this.reviewRepository
                .GetAllAttached()
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.BookId == bookGuid
                                       && r.ApplicationUserId == userGuid);

            if (review != null)
            {
                model.Review = review.Content;                
            }

            return model;
        }

        public async Task<int> GetTotalBooksCountPerUserAsync(string userId)
        {
            IEnumerable<BookApplicationUser> readingList = new List<BookApplicationUser>();

            // check if userId is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // find user
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // get UserBooks and create view model
            readingList = await bookApplicationUserRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(bau => bau.ApplicationUserId == userGuid)
                .ToListAsync();

            return readingList.Count();
        }
    }
}
