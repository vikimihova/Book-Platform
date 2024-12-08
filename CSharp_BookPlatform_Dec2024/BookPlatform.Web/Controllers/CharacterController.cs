using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;

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

        [Authorize]
        [HttpGet]
        public IActionResult Add(string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId))
            {
                return BadRequest();
            }

            AddCharacterInputModel model = new AddCharacterInputModel();
            model.BookId = bookId;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddCharacterInputModel model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            bool result = await this.characterService.AddCharacterAsync(model);

            if (!result)
            {
                return View(model);
            }

            return RedirectToAction("Edit", "ReadingList", new { model.BookId });
        }        
    }
}
