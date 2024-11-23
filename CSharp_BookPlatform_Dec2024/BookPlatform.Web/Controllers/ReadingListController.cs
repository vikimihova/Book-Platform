using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Data.Models;
using BookPlatform.Web.Infrastructure.Extensions;

using static BookPlatform.Common.OutputMessages.ReadingList;

namespace BookPlatform.Web.Controllers
{
    public class ReadingListController : Controller
    {
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReadingListController(
            IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.readingListService = readingListService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ReadingListViewModel> model = await readingListService.GetUserReadingListByUserIdAsync(userId);

            return View(model);
        }

        public async Task<IActionResult> AddToReadingList(string bookId, int readingStatusId)
        {
            // get UserId
            string userId = this.userManager.GetUserId(this.User)!;

            // check UserId StringNullOrEmpty (if false, redirect)
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            // invoke method from ReadingListService
            bool result = await this.readingListService
                .AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            // if false, redirect
            if (result == false)
            {
                TempData[nameof(FailedToAddBookToReadingList)] = FailedToAddBookToReadingList;
            }
            else
            {
                // send temp data for alert message
                TempData[nameof(SuccessfullyAddedToReadingList)] = SuccessfullyAddedToReadingList;
            }            

            return RedirectToAction("Details", "Book", new { bookId });
        }
    }
}
