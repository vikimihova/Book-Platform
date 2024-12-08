using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;

namespace BookPlatform.Web.Controllers
{
    public class CharacterController : Controller
    {
        private readonly ICharacterService characterService;

        public CharacterController(
            ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(string bookId)
        {
            AddCharacterInputModel model;

            try
            {
                model = await this.characterService.GenerateAddCharacterInputModelAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

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

            bool result;

            try
            {
                result = await this.characterService.AddCharacterAsync(model);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }            

            if (!result)
            {
                return RedirectToAction("Edit", "ReadingList", new { model.BookId });
            }

            return RedirectToAction("Edit", "ReadingList", new { model.BookId });
        }        
    }
}
