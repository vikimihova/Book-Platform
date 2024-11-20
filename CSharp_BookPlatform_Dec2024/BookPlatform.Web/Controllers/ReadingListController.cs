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

        public async Task<IActionResult> AddToReadingList(string bookId, string readingStatusId)
        {
            // get UserId

            // check UserId StringNullOrEmpty (if false, redirect)

            // invoke method from ReadingListService

            // if false, redirect

            return RedirectToAction("Details", "Book");
        }
    }
}
