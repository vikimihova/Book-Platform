using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ReadingList;
using BookPlatform.Data.Models;
using BookPlatform.Web.Infrastructure.Extensions;

using static BookPlatform.Common.OutputMessages.ReadingList;
using System.Drawing;

namespace BookPlatform.Web.Controllers
{
    public class ReadingListController : Controller
    {
        private readonly IBaseService baseService;
        private readonly IBookService bookService;
        private readonly ICharacterService characterService;
        private readonly IReadingListService readingListService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReadingListController(
            IBaseService baseService,
            IBookService bookService,
            ICharacterService characterService,
        IReadingListService readingListService,
            UserManager<ApplicationUser> userManager)
        {
            this.baseService = baseService;
            this.bookService = bookService;
            this.characterService = characterService;
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
        public async Task<IActionResult> AddToReadingList(string bookId, int readingStatusId)
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
            string? readingStatusDescription = await baseService.GetReadingStatusAsync(userId, bookId, readingListService);

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
        public async Task<IActionResult> AddToReadingListRead(string bookId, int readingStatusId)
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
            ReadingListInputModel model = new ReadingListInputModel();

            model.BookId = bookId;
            model.BookTitle = book.Title;
            model.ReadingStatus = readingStatusId;
            model.ImageUrl = book.ImageUrl;
            model.Characters = await this.characterService.GetCharactersAsync(bookId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToReadingListRead(ReadingListInputModel model)
        {
            // get UserId
            string userId = this.userManager.GetUserId(this.User)!;

            // check UserId StringNullOrEmpty (if false, redirect)
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            // check model state
            if (!this.ModelState.IsValid) 
            {
                return View(model);
            }

            // try to add book to reading list
            bool result = await this.readingListService.AddBookToUserReadingListReadAsync(model, userId);

            if (result == false)
            {
                return View(model);
            }

            return RedirectToAction("Details", "Book", new { model.BookId });
        }
    }
}
