using BookPlatform.Data.Models;
using BookPlatform.Services.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
                TempData["ErrorMessage"] = "Book already in reading list.";
                return RedirectToAction("Details", "Book", new { id = bookId });
            }
            else
            {
                // send temp data for alert message
                TempData["SuccessMessage"] = "Book successfully added to your reading list!";
            }            

            return RedirectToAction("Details", "Book", new { id = bookId });
        }
    }
}
