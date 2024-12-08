using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;

using static BookPlatform.Common.ApplicationConstants;

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
            if (string.IsNullOrWhiteSpace(bookId))
            {
                return BadRequest();
            }

            IEnumerable<CharacterIndexViewModel> model = await this.characterService.GetCharactersIndexAsync(bookId);

            TempData["BookId"] = bookId;

            return View(model);
        }

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

        // if bool is false?
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

            return RedirectToAction(nameof(Index), new { model.BookId });
        }

        // if bool is false?
        [HttpPost]
        public async Task<IActionResult> Delete(string characterId, string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId) || string.IsNullOrWhiteSpace(characterId))
            {
                return BadRequest();
            }

            bool result = await this.characterService.SoftDeleteCharacterAsync(characterId, bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }

        // if bool is false?
        [HttpPost]
        public async Task<IActionResult> Include(string characterId, string bookId)
        {
            if (string.IsNullOrWhiteSpace(bookId) || string.IsNullOrWhiteSpace(characterId))
            {
                return BadRequest();
            }

            bool result = await this.characterService.IncludeCharacterAsync(characterId, bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }
    }
}
