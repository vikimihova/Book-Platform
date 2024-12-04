using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            ApplicationUser? user = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id.ToString().ToLower() == userId.ToLower());

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

        public async Task<IEnumerable<ApplicationUserViewModel>> FindFriendAsync(string friendEmail)
        {
            IEnumerable<ApplicationUserViewModel> model = new List<ApplicationUserViewModel>();

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                return model;
            }

            // find user
            ApplicationUser? user = await this.userManager.FindByEmailAsync(friendEmail);

            if (user == null)
            {
                return model;
            }

            model = new List<ApplicationUserViewModel>()
            {
                new ApplicationUserViewModel()
                {
                    Email = user.Email!,
                    UserName = user.UserName!,
                }
            }
            .ToList();

            return model;
        }

        public async Task<bool> AddFriendAsync(string mainUserId, string friendEmail)
        {
            // check if user id is a valid guid
            Guid mainUserGuid = Guid.Empty;
            if (!IsGuidValid(mainUserId, ref mainUserGuid))
            {
                return false;
            }

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                return false;
            }

            // find users
            ApplicationUser? mainUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id.ToString().ToLower() == mainUserId.ToLower());

            ApplicationUser? friendUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Email == friendEmail);

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

        public async Task<bool> RemoveFriendAsync(string mainUserId, string friendEmail)
        {
            // check if user id is a valid guid
            Guid mainUserGuid = Guid.Empty;
            if (!IsGuidValid(mainUserId, ref mainUserGuid))
            {
                return false;
            }

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                return false;
            }

            // find users
            ApplicationUser? mainUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id.ToString().ToLower() == mainUserId.ToLower());

            ApplicationUser? friendUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Email == friendEmail);

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
