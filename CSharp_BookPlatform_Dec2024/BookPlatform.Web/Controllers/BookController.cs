using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using BookPlatform.Data.Models;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(
            IBookService _bookService,
            IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.bookService = _bookService;
            this.readingListService = readingListService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> model =
                await this.bookService.IndexGetAllAsync();

            return View(model);
        }

        public async Task<IActionResult> Details(string bookId)
        {
            // set TempData for reading status
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                ReadingStatus? readingStatus = null;

                ApplicationUser? currentUser = await userManager.FindByNameAsync(this.User.Identity.Name!);

                if (currentUser != null)
                {
                    // invoke method from ReadingListService

                    readingStatus = await this.readingListService.GetReadingStatusForCurrentBookApplicationUserAsync(bookId, currentUser.Id);
                }

                if (readingStatus != null)
                {
                    TempData["ReadingStatus"] = readingStatus.StatusDescription;
                } 
            }

            // generate view model
            BookDetailsViewModel? model = await this.bookService.GetBookDetailsAsync(bookId);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
    }
}
