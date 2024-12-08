using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ApplicationUser;

using BookPlatform.Web.Infrastructure.Extensions;

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
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // get user id
            string userId = User.GetUserId()!;
            
            ICollection<ApplicationUserViewModel> model = await this.friendService.GetFriendsAsync(userId);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find(string email)
        {
            // check email
            if (String.IsNullOrWhiteSpace(email))
            {
                return RedirectToAction(nameof(Index));
            }

            // get user id
            string userId = User.GetUserId()!;            

            ICollection<ApplicationUserViewModel> model = await this.friendService.FindFriendAsync(userId, email);

            return View("Index", model);
        }

        // if bool is false?
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(string email)
        {
            // check email
            if (String.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            // ref URL
            var refererUrl = Request.Headers["Referer"].ToString();

            // get user id
            string userId = User.GetUserId()!;            

            bool result = await this.friendService.AddFriendAsync(userId, email);

            return Redirect(refererUrl);
        }

        // if bool is false?
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(string email)
        {
            // check email
            if (String.IsNullOrWhiteSpace(email))
            {
                return BadRequest();
            }

            // ref URL
            var refererUrl = Request.Headers["Referer"].ToString();

            // get user id
            string userId = User.GetUserId()!;                       

            bool result = await this.friendService.RemoveFriendAsync(userId, email);

            return Redirect(refererUrl);
        }
    }
}
