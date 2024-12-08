using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Rating;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Core.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRepository<Rating, int> ratingRepository;

        public RatingService(IRepository<Rating, int> ratingRepository)
        {
            this.ratingRepository = ratingRepository;
        }

        public async Task<ICollection<SelectRatingViewModel>> GetRatingsAsync()
        {
            var ratings = await this.ratingRepository
                .GetAllAttached()
                .AsNoTracking()
                .Select(r => new SelectRatingViewModel()
                {
                    Id = r.Id,
                    Description = r.RatingDescription
                })
                .ToListAsync();

            return ratings;
        }
    }
}
