using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddBookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            try
            {
                bool result = await this.bookService.AddBookAsync(model);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }                      

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string bookId)
        {
            EditBookInputModel model;

            try
            {
                model = await this.bookService.GenerateEditBookInputModelAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            model.Authors = await this.authorService.GetAuthorsAsync();
            model.Genres = await this.genreService.GetGenresAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBookInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Authors = await this.authorService.GetAuthorsAsync();
                model.Genres = await this.genreService.GetGenresAsync();

                return View(model);
            }

            try
            {
                bool result = await this.bookService.EditBookAsync(model);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string bookId)
        {
            bool result;

            try
            {
                result = await this.bookService.SoftDeleteBookAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }            

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Include(string bookId)
        {
            bool result;

            try
            {
                result = await this.bookService.IncludeBookAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
