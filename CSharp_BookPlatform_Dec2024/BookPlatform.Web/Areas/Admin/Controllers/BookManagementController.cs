using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class BookManagementController : Controller
    {
        private readonly IBookService bookService;
        private readonly IAuthorService authorService;
        private readonly IGenreService genreService;

        public BookManagementController(
            IBookService bookService,
            IAuthorService authorService,
            IGenreService genreService)
        {
            this.bookService = bookService;
            this.authorService = authorService;
            this.genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> model = await this.bookService
                .IndexGetAllAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddBookInputModel model = new AddBookInputModel();

            model.Authors = await this.authorService.GetAuthorsAsync();
            model.Genres = await this.genreService.GetGenresAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            bool result = await this.bookService.AddBookAsync(model);

            if (result == false)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string bookId)
        {
            EditBookInputModel? model = await this.bookService.GenerateEditBookInputModelAsync(bookId);

            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }

            model.Authors = await this.authorService.GetAuthorsAsync();
            model.Genres = await this.genreService.GetGenresAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            bool result = await this.bookService.EditBookAsync(model);

            if (!result)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string bookId)
        {
            bool result = await this.bookService.SoftDeleteBookAsync(bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Include(string bookId)
        {
            bool result = await this.bookService.IncludeBookAsync(bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
