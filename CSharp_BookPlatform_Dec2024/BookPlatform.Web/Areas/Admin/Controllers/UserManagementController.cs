using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Admin.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            bool result = await this.userService.MakeAdminAsync(userId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveAdmin(string userId)
        {
            bool result = await this.userService.RemoveAdminAsync(userId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            bool result = await this.userService.DeleteUserAsync(userId);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
