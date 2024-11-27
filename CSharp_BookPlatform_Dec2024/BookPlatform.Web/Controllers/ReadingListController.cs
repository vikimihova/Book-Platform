using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Data.Models;
using BookPlatform.Web.Infrastructure.Extensions;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.OutputMessages.ReadingList;
using static BookPlatform.Common.ModelValidationErrorMessages.DateTimeFormats;

using System.Globalization;

namespace BookPlatform.Web.Controllers
{
    public class ReadingListController : Controller
    {
        private readonly IBaseService baseService;
        private readonly IBookService bookService;
        private readonly ICharacterService characterService;
        private readonly IRatingService ratingService;
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReadingListController(
            IBaseService baseService,
            IBookService bookService,
            ICharacterService characterService,
            IRatingService ratingService,
            IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.baseService = baseService;
            this.bookService = bookService;
            this.characterService = characterService;
            this.ratingService = ratingService;
            this.readingListService = readingListService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ReadingListViewModel> model = await readingListService.GetUserReadingListByUserIdAsync(userId);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add(string bookId, int readingStatusId)
        {
            // get UserId
            string userId = this.userManager.GetUserId(this.User)!;

            // check UserId StringNullOrEmpty (if false, redirect)
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            // check if book already read
            bool IsAlreadyRead = await this.readingListService.CheckIfBookAlreadyReadAsync(bookId, userId, readingStatusId);

            if (IsAlreadyRead)
            {
                return RedirectToAction("Details", "Book", new { bookId });
            }

            // invoke method from ReadingListService
            bool result = await this.readingListService
                .AddBookToUserReadingListAsync(bookId, userId, readingStatusId);

            // get reading status
            string? readingStatusDescription = await this.readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);

            // if false, redirect
            if (result == false)
            {
                TempData[nameof(BookAlreadyInReadingList)] = string.Format(BookAlreadyInReadingList, readingStatusDescription);
            }
            else
            {
                // send temp data for alert message
                TempData[nameof(SuccessfullyAddedToReadingList)] = string.Format(SuccessfullyAddedToReadingList, readingStatusDescription);
            }            

            return RedirectToAction("Details", "Book", new { bookId });
        }

        [HttpGet]
        public async Task<IActionResult> AddAsRead(string bookId, int readingStatusId)
        {
            // check if book exists
            Book? book = await this.bookService.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return RedirectToAction("Index", "Book");
            }

            // check if book already read
            string userId = this.userManager.GetUserId(this.User)!;            
            bool IsAlreadyRead = await this.readingListService.CheckIfBookAlreadyReadAsync(bookId, userId, readingStatusId);
            
            if (IsAlreadyRead)
            {
                return RedirectToAction("Details", "Book", new { bookId });
            }

            // create input model to pass book information        
            ReadingListAddInputModel model = this.readingListService.GenerateAddInputModel(book, readingStatusId);
            model.Characters = await this.characterService.GetCharactersAsync(bookId);
            model.Ratings = await this.ratingService.GetRatingsAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsRead(ReadingListAddInputModel model)
        {
            // check userId
            string userId = this.userManager.GetUserId(this.User)!;

            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            // check model state
            if (!this.ModelState.IsValid) 
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            // check date formats
            if (model.DateStarted != null)
            {
                if (!DateTime.TryParseExact(model.DateStarted, DateViewFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateStarted))
                {
                    ModelState.AddModelError(nameof(model.DateStarted), WrongDateViewFormat);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }

                if (dateStarted > DateTime.Today)
                {
                    ModelState.AddModelError(nameof(model.DateStarted), DateInFuture);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }
            }

            if (model.DateFinished != null)
            {
                if (!DateTime.TryParseExact(model.DateFinished, DateViewFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished))
                {
                    ModelState.AddModelError(nameof(model.DateFinished), WrongDateViewFormat);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }

                if (dateFinished > DateTime.Today)
                {
                    ModelState.AddModelError(nameof(model.DateFinished), DateInFuture);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }
            }        

            // try to add book to reading list
            bool result = await this.readingListService.AddBookToUserReadingListReadAsync(model, userId);

            if (result == false)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            return RedirectToAction("Details", "Book", new { model.BookId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string bookId, int readingStatusId)
        {            
            // check if book exists
            Book? book = await this.bookService.GetBookByIdAsync(bookId);

            if (book == null)
            {
                return RedirectToAction("Index", "Book");
            }

            // check if book already read
            string userId = this.userManager.GetUserId(this.User)!;

            bool IsAlreadyRead = await this.readingListService.CheckIfBookAlreadyReadAsync(bookId, userId, readingStatusId);

            if (!IsAlreadyRead)
            {
                return RedirectToAction("Details", "Book", new { bookId });
            }

            // create input model to pass book information        
            ReadingListEditInputModel model = await this.readingListService.GenerateEditInputModelAsync(bookId, userId);
            model.Characters = await this.characterService.GetCharactersAsync(bookId);
            model.Ratings = await this.ratingService.GetRatingsAsync();

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ReadingListEditInputModel model)
        {
            // check userId
            string userId = this.userManager.GetUserId(this.User)!;

            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            // check model state
            if (!this.ModelState.IsValid)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            // check date formats
            if (model.DateStarted != null)
            {
                if (!DateTime.TryParseExact(model.DateStarted, DateViewFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateStarted))
                {
                    ModelState.AddModelError(nameof(model.DateStarted), WrongDateViewFormat);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }

                if (dateStarted > DateTime.Today)
                {
                    ModelState.AddModelError(nameof(model.DateStarted), DateInFuture);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }
            }

            if (model.DateFinished != null)
            {
                if (!DateTime.TryParseExact(model.DateFinished, DateViewFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished))
                {
                    ModelState.AddModelError(nameof(model.DateFinished), WrongDateViewFormat);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }

                if (dateFinished > DateTime.Today)
                {
                    ModelState.AddModelError(nameof(model.DateFinished), DateInFuture);
                    model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                    model.Ratings = await this.ratingService.GetRatingsAsync();
                    return View(model);
                }
            }

            // try to update book entry in reading list
            bool result = await this.readingListService.EditInReadingListAsync(model, userId);

            if (result == false)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Remove(string bookId)
        {
            string userId = this.userManager.GetUserId(this.User)!;

            bool result = await this.readingListService
                .RemoveBookFromUserReadingListAsync(bookId, userId);

            if (result == false)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
