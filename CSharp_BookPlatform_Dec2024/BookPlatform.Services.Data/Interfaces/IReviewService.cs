using BookPlatform.Core.ViewModels.Review;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAllReviewsPerBookAsync(string bookId);
    }
}
