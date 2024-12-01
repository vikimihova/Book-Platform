using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
            IEnumerable<CharacterIndexViewModel> model = await this.characterService
                .GetCharactersIndexAsync(bookId);

            TempData["BookId"] = bookId;

            return View(model);
        }

        [HttpGet]
        public IActionResult Add(string bookId)
        {
            AddCharacterInputModel model = new AddCharacterInputModel();
            model.BookId = bookId;

            return View(model);
        }

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

        [HttpPost]
        public async Task<IActionResult> Delete(string characterId, string bookId)
        {
            bool result = await this.characterService.SoftDeleteCharacterAsync(characterId, bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }

        [HttpPost]
        public async Task<IActionResult> Include(string characterId, string bookId)
        {
            bool result = await this.characterService.IncludeCharacterAsync(characterId, bookId);

            if (!result)
            {
                return RedirectToAction(nameof(Index), new { bookId });
            }

            return RedirectToAction(nameof(Index), new { bookId });
        }
    }
}
