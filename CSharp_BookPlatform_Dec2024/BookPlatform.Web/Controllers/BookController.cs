using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Data.Models;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Web.Controllers
{    
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly IGenreService genreService;
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookController(
            IBookService bookService,
            IGenreService genreService,
            IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.bookService = bookService;
            this.genreService = genreService;
            this.readingListService = readingListService;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(BookIndexViewModelWrapper inputModel)
        {
            IEnumerable<BookIndexViewModel> books =
                await this.bookService.IndexGetAllRandomAsync(inputModel);

            BookIndexViewModelWrapper model = new BookIndexViewModelWrapper();
            model.Books = books;
            model.Genres = await this.genreService.GetGenresAsync();

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(string bookId)
        {           
            // set TempData for reading status
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                // get UserId
                string userId = this.userManager.GetUserId(this.User)!;

                // get reading status
                string? readingStatusDescription;

                try
                {
                    readingStatusDescription = await this.readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);
                }
                catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
                {
                    return BadRequest();
                }                

                if (readingStatusDescription != null)
                {
                    TempData["ReadingStatus"] = readingStatusDescription;
                }
            }

            // generate view model
            BookDetailsViewModel model;

            try
            {
                model = await this.bookService.GetBookDetailsAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return View(model);
        }        
    }
}
