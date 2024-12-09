using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Review;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Core.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly IRepository<Review, Guid> reviewRepository;
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewService(
            IRepository<Review, Guid> reviewRepository,
            IRepository<Book, Guid> bookRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewRepository = reviewRepository;
            this.bookRepository = bookRepository;
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllNewReviewsAsync(string userId)
        {
            // check if userId is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }                   

            // find user
            ApplicationUser? user = await this.userManager
                .Users
                .AsNoTracking()
                .Include(u => u.UserBooks)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // generate empty model
            IEnumerable<ReviewViewModel> reviews = new List<ReviewViewModel>();

            if (user.LastLogin == null)
            {
                return reviews;
            }

            // get bookIds for UserBooks
            var userBookIds = user.UserBooks.Select(ub => ub.BookId).ToList();

            // generate review view models
            reviews = await this.reviewRepository
                .GetAllAttached()        
                .AsNoTracking()
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.Character)
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.Book)
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.ApplicationUser)
                .Where(r => r.ApplicationUserId != userGuid &&
                            userBookIds.Contains(r.BookApplicationUser.BookId) &&
                            (r.CreatedOn > user.LastLogin || r.ModifiedOn > user.LastLogin) &&
                            r.BookApplicationUser.Book.IsDeleted == false)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id.ToString(),
                    BookId = r.BookId.ToString(),
                    Title = r.BookApplicationUser.Book.Title,
                    Content = r.Content,
                    IsModified = r.ModifiedOn != null ? true : false,
                    Author = r.BookApplicationUser.ApplicationUser.UserName!,
                    AuthorEmail = r.BookApplicationUser.ApplicationUser.Email!,
                    Rating = r.BookApplicationUser.RatingId != null ? r.BookApplicationUser.RatingId : null,
                    FavoriteCharacter = r.BookApplicationUser.Character != null ? r.BookApplicationUser.Character.Name : null,
                })
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAllReviewsPerBookAsync(string bookId)
        {
            // check if bookId is a valid guid
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // find book
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            // generate review view models
            IEnumerable<ReviewViewModel> reviews = await this.reviewRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.Character)
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.Book)
                .Include(r => r.BookApplicationUser)
                .ThenInclude(bau => bau.ApplicationUser)
                .Where(r => r.BookId == bookGuid)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id.ToString(),
                    BookId = r.BookId.ToString(),
                    Title = r.BookApplicationUser.Book.Title,
                    Content = r.Content,
                    IsModified = r.ModifiedOn != null ? true : false,
                    Author = r.BookApplicationUser.ApplicationUser.UserName!,
                    AuthorEmail = r.BookApplicationUser.ApplicationUser.Email!,
                    Rating = r.BookApplicationUser.RatingId != null ? r.BookApplicationUser.RatingId : null,
                    FavoriteCharacter = r.BookApplicationUser.Character != null ? r.BookApplicationUser.Character.Name : null,
                })
                .ToListAsync();

            return reviews;
        }
    }
}
