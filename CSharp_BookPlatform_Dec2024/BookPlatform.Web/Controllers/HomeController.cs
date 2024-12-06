using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using BookPlatform.Data.Models;
using BookPlatform.Core.ViewModels.Discover;
using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Web.Infrastructure.Extensions;

namespace BookPlatform.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IReadingListService readingListService;
        private readonly IReviewService reviewService;

        public HomeController(
            ILogger<HomeController> logger,
            IReadingListService readingListService,
            IReviewService reviewService)
        {
            this.logger = logger;
            this.readingListService = readingListService;
            this.reviewService = reviewService;
        }

        public IActionResult Index()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false) 
            {
                return RedirectToAction("Index", "Book");
            }

            return View();
        }

        public async Task<IActionResult> Discover()
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            DiscoverViewModel model = new DiscoverViewModel();

            model.NewReviews = await this.reviewService.GetAllNewReviewsAsync(userId);
            model.FriendBooks = await this.readingListService.GetFriendBooksByUserIdAsync(userId);

            return View(model);
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return this.View("PageNotFound");
            }

            if (statusCode == 400)
            {
                return this.View("BadRequest");
            }

            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
