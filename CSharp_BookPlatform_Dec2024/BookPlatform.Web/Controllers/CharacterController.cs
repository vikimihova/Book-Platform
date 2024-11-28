using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ICharacterService characterService;
        private readonly IBookService bookService;

        public CharacterController(
            ICharacterService characterService,
            IBookService bookService)
        {
            this.characterService = characterService;
            this.bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(string bookId)
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.Books = await this.bookService.GetBooksAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCharacterInputModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Books = await this.bookService.GetBooksAsync();
                return View(model);
            }

            bool result = await this.characterService.AddCharacterAsync(model);

            if (!result)
            {
                model.Books = await this.bookService.GetBooksAsync();
                return View(model);
            }

            return RedirectToAction("Edit", "ReadingList", new { model.BookId });
        }

        [HttpGet]
        public async Task<IActionResult> SoftDelete(string characterId)
        {
            bool result = await this.characterService.SoftDeleteCharacterAsync(characterId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
