using BookPlatform.Services.Data.Interfaces;
using BookPlatform.Web.ViewModels.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService _bookService)
        {
            this.bookService = _bookService;            
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<BookIndexViewModel> model =
                await this.bookService.IndexGetAllAsync();

            return View(model);
        }
    }
}
