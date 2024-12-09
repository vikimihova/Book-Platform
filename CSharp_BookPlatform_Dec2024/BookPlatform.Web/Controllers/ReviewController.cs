using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Review;

using BookPlatform.Web.Infrastructure.Extensions;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Web.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;
        private readonly IBookService bookService;

        public ReviewController(
            IReviewService reviewService,
            IBookService bookService)
        {
            this.reviewService = reviewService;
            this.bookService = bookService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get user id
            string userId = User.GetUserId()!;

            // generate view model
            IEnumerable<ReviewViewModel> model;      
            
            try
            {
                model = await this.reviewService.GetAllNewReviewsAsync(userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }         

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AllBookReviews(string bookId)
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

            // generate view model for reviews
            IEnumerable<ReviewViewModel> reviewsModel;

            try
            {
                reviewsModel = await this.reviewService.GetAllReviewsPerBookAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            // generate main model
            AllBookReviewsViewModel model = new AllBookReviewsViewModel();
            model.BookDetails = bookModel;
            model.Reviews = reviewsModel;

            return View(model);
        }        
    }
}
