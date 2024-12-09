using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;

using static BookPlatform.Common.ApplicationConstants;
using BookPlatform.Data.Models;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class CharacterManagementController : Controller
    {
        private readonly ICharacterService characterService;

        public CharacterManagementController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string bookId)
        {
            IEnumerable<CharacterIndexViewModel> model;

            try
            {
                model = await this.characterService.GetCharactersIndexAsync(bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            TempData["BookId"] = bookId;

            return View(model);
        }

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

            return View(model);
        }
                
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddCharacterInputModel model)
        {
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            bool result;

            try
            {
                result = await this.characterService.AddCharacterByAdminAsync(model);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { model.BookId });
            }

            return RedirectToAction(nameof(Index), new { model.BookId });
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(string characterId, string bookId)
        {
            bool result;

            try
            {
                result = await this.characterService.SoftDeleteCharacterAsync(characterId, bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }            

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }

        
        [HttpPost]
        public async Task<IActionResult> Include(string characterId, string bookId)
        {
            bool result;

            try
            {
                result = await this.characterService.IncludeCharacterAsync(characterId, bookId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }
    }
}
