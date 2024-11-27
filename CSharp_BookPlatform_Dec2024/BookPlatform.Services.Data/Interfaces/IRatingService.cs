using BookPlatform.Core.ViewModels.Rating;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IRatingService
    {
        Task<ICollection<SelectRatingViewModel>> GetRatingsAsync();
    }
}
