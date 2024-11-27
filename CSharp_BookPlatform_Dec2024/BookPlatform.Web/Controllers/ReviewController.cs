using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Review;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewController(
            IReviewService reviewService,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewService = reviewService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllBookReviews(string bookId)
        {
            IEnumerable<ReviewViewModel> model = await this.reviewService.GetAllReviewsPerBookAsync(bookId);

            return View(model);
        }
    }
}
