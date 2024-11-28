using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BookPlatform.Core.Services
{
    public class FriendService : BaseService, IFriendService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public FriendService(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUserViewModel>> GetFriendsAsync(string userId)
        {
            IEnumerable<ApplicationUserViewModel> model = new List<ApplicationUserViewModel>();

            // check if user id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                return model;
            }

            // find user
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return model;
            }

            // check if user has any friends
            if (!user.Friends.Any())
            {
                return model;
            }

            // generate view model
            model = user.Friends
                .Select(u => new ApplicationUserViewModel()
                {
                    Email = u.Email!,
                    UserName = u.UserName!
                })
                .ToList();

            return model;
        }

        public async Task<ApplicationUserViewModel?> FindFriendAsync(string friendUserName)
        {
            // check username
            if (String.IsNullOrWhiteSpace(friendUserName))
            {
                return null;
            }

            // find user
            ApplicationUser? user = await this.userManager.FindByNameAsync(friendUserName);

            if (user == null)
            {
                return null;
            }

            // generate view model
            ApplicationUserViewModel model = new ApplicationUserViewModel()
            {
                Email = user.Email!,
                UserName = user.UserName!,
            };

            return model;
        }

        public async Task<bool> AddFriendAsync(string mainUserId, string friendUserName)
        {
            // check if user id is a valid guid
            Guid mainUserGuid = Guid.Empty;
            if (!IsGuidValid(mainUserId, ref mainUserGuid))
            {
                return false;
            }

            // check username
            if (String.IsNullOrWhiteSpace(friendUserName))
            {
                return false;
            }

            // find users
            ApplicationUser? mainUser = await this.userManager.FindByIdAsync(mainUserId);
            ApplicationUser? friendUser = await this.userManager.FindByNameAsync(friendUserName);

            if (mainUser == null || friendUser == null)
            {
                return false;
            }

            // add to friends
            mainUser.Friends.Add(friendUser);
            friendUser.Friends.Add(mainUser);

            await userManager.UpdateAsync(mainUser);
            await userManager.UpdateAsync(friendUser);

            return true;
        }

        public async Task<bool> RemoveFriendAsync(string mainUserId, string friendUserName)
        {
            // check if user id is a valid guid
            Guid mainUserGuid = Guid.Empty;
            if (!IsGuidValid(mainUserId, ref mainUserGuid))
            {
                return false;
            }

            // check username
            if (String.IsNullOrWhiteSpace(friendUserName))
            {
                return false;
            }

            // find users
            ApplicationUser? mainUser = await this.userManager.FindByIdAsync(mainUserId);
            ApplicationUser? friendUser = await this.userManager.FindByNameAsync(friendUserName);

            if (mainUser == null || friendUser == null)
            {
                return false;
            }

            // remove from friends
            mainUser.Friends.Remove(friendUser);
            friendUser.Friends.Remove(mainUser);

            await userManager.UpdateAsync(mainUser);
            await userManager.UpdateAsync(friendUser);

            return true;
        }
    }
}
