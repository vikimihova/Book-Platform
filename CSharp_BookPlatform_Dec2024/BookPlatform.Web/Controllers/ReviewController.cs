using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Review;
using BookPlatform.Data.Models;
using BookPlatform.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ReviewViewModel> model = await this.reviewService.GetAllNewReviewsAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AllBookReviews(string bookId)
        {
            IEnumerable<ReviewViewModel> model = await this.reviewService.GetAllReviewsPerBookAsync(bookId);

            return View(model);
        }        
    }
}
