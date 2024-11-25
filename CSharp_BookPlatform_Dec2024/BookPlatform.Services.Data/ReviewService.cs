using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Core.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        public ReviewService(
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            
        }

        
    }
}
