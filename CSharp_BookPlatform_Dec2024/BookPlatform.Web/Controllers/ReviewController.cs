using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Review;

using BookPlatform.Web.Infrastructure.Extensions;

namespace BookPlatform.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(
            IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get user id
            string userId = User.GetUserId()!;            

            // generate view model
            IEnumerable<ReviewViewModel> model = await this.reviewService.GetAllNewReviewsAsync(userId);

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllBookReviews(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                return BadRequest();
            }

            IEnumerable<ReviewViewModel> model = await this.reviewService.GetAllReviewsPerBookAsync(bookId);

            return View(model);
        }        
    }
}
