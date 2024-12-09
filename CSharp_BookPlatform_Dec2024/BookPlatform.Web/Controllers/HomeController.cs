using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.ViewModels;
using BookPlatform.Core.ViewModels.Discover;
using BookPlatform.Core.Services.Interfaces;

using BookPlatform.Web.Infrastructure.Extensions;

using static BookPlatform.Common.ApplicationConstants;

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

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            if ((this.User?.Identity?.IsAuthenticated ?? false) &&
                 this.User.IsInRole(UserRoleName))
            {
                return RedirectToAction("Discover", "Home");
            }

            if (this.User?.Identity?.IsAuthenticated ?? false) 
            {
                return RedirectToAction("Index", "Book");
            }

            return View();
        }

        [Authorize(Roles = UserRoleName)]
        [HttpGet]
        public async Task<IActionResult> Discover()
        {
            // get user id
            string userId = User.GetUserId()!;            

            DiscoverViewModel model = new DiscoverViewModel();

            model.NewReviews = await this.reviewService.GetAllNewReviewsAsync(userId);
            model.FriendBooks = await this.readingListService.GetFriendBooksByUserIdAsync(userId);

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
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

            if (statusCode == 500)
            {
                return this.View("InternalServerError");
            }

            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
