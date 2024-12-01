using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using BookPlatform.Data.Models;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Core.Services;

namespace BookPlatform.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBaseService baseService;
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(
            IBaseService baseService,
            IBookService bookService,
            IAuthorService authorService,
            IGenreService genreService,
            IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.baseService = baseService;
            this.bookService = bookService;
            this.authorService = authorService;
            this.genreService = genreService;
            this.readingListService = readingListService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> model =
                await this.bookService.IndexGetAllRandomAsync();

            return View(model);
        }
               
        [HttpGet]
        public async Task<IActionResult> Details(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                throw new ArgumentNullException(nameof(bookId));
            }

            // set TempData for reading status
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                // get UserId
                string userId = this.userManager.GetUserId(this.User)!;

                // get reading status
                string? readingStatusDescription = await this.readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);

                if (readingStatusDescription != null)
                {
                    TempData["ReadingStatus"] = readingStatusDescription;
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
