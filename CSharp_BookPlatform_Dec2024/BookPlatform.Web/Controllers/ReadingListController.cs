using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

using BookPlatform.Data.Models;
using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Web.Infrastructure.Extensions;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.OutputMessages.ReadingList;
using static BookPlatform.Common.ModelValidationErrorMessages.DateTimeFormats;
using System.Net;


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
        public async Task<IActionResult> Index(ReadingListPaginatedViewModel inputModel)
        {
            // get user id
            string userId = User.GetUserId()!;

            // generate view model for reading list
            IEnumerable<ReadingListViewModel> booksModel;

            try
            {
                booksModel = await readingListService.GetUserReadingListByUserIdAsync(userId, inputModel);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            // generate main view model
            ReadingListPaginatedViewModel model = new ReadingListPaginatedViewModel();
            model.Books = booksModel;
            model.totalBooksPerUserCount = await this.readingListService.GetTotalBooksCountPerUserAsync(userId);
            model.CurrentPage = inputModel.CurrentPage;
            model.EntitiesPerPage = inputModel.EntitiesPerPage;
            model.TotalPages = (int)Math.Ceiling((double)model.totalBooksPerUserCount / inputModel.EntitiesPerPage!.Value);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> IndexFriends()
        {
            // get user id
            string userId = User.GetUserId()!;

            // generate view model
            ICollection<FriendBookViewModel> model;

            try
            {
                model = await this.readingListService.GetFriendBooksByUserIdAsync(userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(string bookId, int readingStatusId)
        {
            // get UserId
            string userId = this.userManager.GetUserId(this.User)!;

            // add book to reading list
            bool result = false;

            try
            {
                result = await this.readingListService.AddBookToUserReadingListAsync(bookId, userId, readingStatusId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }
            
            // get reading status
            string? readingStatusDescription = await this.readingListService.GetCurrentReadingStatusDescriptionAsync(bookId, userId);
                        
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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddAsRead(string bookId, int readingStatusId)
        {           
            // get userId
            string userId = this.userManager.GetUserId(this.User)!;

            // create input model to pass book information 
            ReadingListAddInputModel? model = null;

            try 
            {                       
                model = await this.readingListService.GenerateAddInputModelAsync(bookId, userId, readingStatusId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }            

            if (model == null)
            {
                return RedirectToAction("Details", "Book", new { bookId });
            }

            model.Characters = await this.characterService.GetCharactersAsync(bookId);
            model.Ratings = await this.ratingService.GetRatingsAsync();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAsRead(ReadingListAddInputModel model)
        {           
            // check model state
            if (!this.ModelState.IsValid) 
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }            

            if (model.DateFinished != null)
            {
                if (!DateTime.TryParseExact(model.DateFinished, DateInputFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished))
                {
                    ModelState.AddModelError(nameof(model.DateFinished), WrongDateInputFormat);
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

            // get userId
            string userId = this.userManager.GetUserId(this.User)!;

            // try to add book to reading list
            bool result = false;

            try
            {
                result = await this.readingListService.AddBookToUserReadingListReadAsync(model, userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }             

            if (result == false)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            return RedirectToAction("Details", "Book", new { model.BookId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(string bookId, int readingStatusId)
        {            
            // get userId
            string userId = this.userManager.GetUserId(this.User)!;

            // create input model to pass book information        
            ReadingListEditInputModel? model = null;

            try
            {
                model = await this.readingListService.GenerateEditInputModelAsync(bookId, userId, readingStatusId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (model == null)
            {
                return RedirectToAction("Details", "Book", new { bookId });
            }

            model.Characters = await this.characterService.GetCharactersAsync(bookId);
            model.Ratings = await this.ratingService.GetRatingsAsync();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReadingListEditInputModel model)
        {
            // check model state
            if (!this.ModelState.IsValid)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }           

            if (model.DateFinished != null)
            {
                if (!DateTime.TryParseExact(model.DateFinished, DateInputFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateFinished))
                {
                    ModelState.AddModelError(nameof(model.DateFinished), WrongDateInputFormat);
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

            // get userId
            string userId = this.userManager.GetUserId(this.User)!;

            // try to update book entry in reading list

            bool result = false;

            try
            {
                result = await this.readingListService.EditInReadingListAsync(model, userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (result == false)
            {
                model.Characters = await this.characterService.GetCharactersAsync(model.BookId);
                model.Ratings = await this.ratingService.GetRatingsAsync();
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
                
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(string bookId)
        {           
            string userId = this.userManager.GetUserId(this.User)!;

            bool result = false;

            try
            {
                result = await this.readingListService.RemoveBookFromUserReadingListAsync(bookId, userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (result == false)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
