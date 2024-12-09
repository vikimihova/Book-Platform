using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Book;
using BookPlatform.Core.ViewModels.Quote;
using BookPlatform.Core.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class QuoteController : Controller
    {
        private readonly IQuoteService quoteService;
        private readonly IBookService bookService;

        public QuoteController(
            IQuoteService quoteService,
            IBookService bookService)
        {
            this.quoteService = quoteService;
            this.bookService = bookService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllBookQuotes(string bookId)
        {
            // generate view model for book details
            BookDetailsViewModel bookModel;

            try
            {
                bookModel = await this.bookService.GetBookDetailsAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            // generate view model for quotes
            IEnumerable<QuoteViewModel> quotesModel;

            try
            {
                quotesModel = await this.quoteService.GetAllQuotesPerBookAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            // generate main model
            AllBookQuotesViewModel model = new AllBookQuotesViewModel();
            model.BookDetails = bookModel;
            model.Quotes = quotesModel;

            return View(model);
        }
    }
}
