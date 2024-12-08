using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Admin.UserManagement;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    [Area(AdminRoleName)]
    [Authorize(Roles = AdminRoleName)]
    public class UserManagementController : Controller
    {
        private readonly IUserService userService;

        public UserManagementController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<AllApplicationUsersViewModel> model = await this.userService
                .GetAllUsersAsync();

            return this.View(model);
        }
                
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string userId)
        {
            bool result;

            try
            {
                result = await this.userService.MakeAdminAsync(userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string userId)
        {
            bool result;

            try
            {
                result = await this.userService.RemoveAdminAsync(userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool result;

            try
            {
                result = await this.userService.DeleteUserAsync(userId);
            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
