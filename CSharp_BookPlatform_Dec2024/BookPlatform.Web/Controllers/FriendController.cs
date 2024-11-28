using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Data.Models;
using BookPlatform.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService friendService;

        public FriendController(IFriendService friendService)
        {
            this.friendService = friendService;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            IEnumerable<ApplicationUserViewModel> model = await this.friendService.GetFriendsAsync(userId);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find(string userName)
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            ApplicationUserViewModel? model = await this.friendService.FindFriendAsync(userName);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddFriend(string userName)
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.friendService.AddFriendAsync(userId, userName);

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> RemoveFriend(string userName)
        {
            // get user id
            string? userId = User.GetUserId();

            // check if user is authenticated
            if (String.IsNullOrWhiteSpace(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            bool result = await this.friendService.RemoveFriendAsync(userId, userName);

            return RedirectToAction(nameof(Index));
        }
    }
}
