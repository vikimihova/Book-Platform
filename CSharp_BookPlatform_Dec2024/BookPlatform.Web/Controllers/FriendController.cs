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

            ICollection<ApplicationUserViewModel> model;

            try
            {
                model = await this.friendService.GetFriendsAsync(userId);

            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Find(string email)
        {          
            // get user id
            string userId = User.GetUserId()!;

            ICollection<ApplicationUserViewModel> model;

            try
            {
                model = await this.friendService.FindFriendAsync(userId, email);

            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return View("Index", model);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add(string email)
        {          
            // ref URL
            var refererUrl = Request.Headers["Referer"].ToString();

            // get user id
            string userId = User.GetUserId()!;

            // try adding friend
            bool result;

            try
            {
                result = await this.friendService.AddFriendAsync(userId, email);

            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return Redirect(refererUrl);
        }
                
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Remove(string email)
        {
            // ref URL
            var refererUrl = Request.Headers["Referer"].ToString();

            // get user id
            string userId = User.GetUserId()!;

            // try removing friend
            bool result;

            try
            {
                result = await this.friendService.RemoveFriendAsync(userId, email);

            }
            catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
            {
                return BadRequest();
            }

            return Redirect(refererUrl);
        }
    }
}
