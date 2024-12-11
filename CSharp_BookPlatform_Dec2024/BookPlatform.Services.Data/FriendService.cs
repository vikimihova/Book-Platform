using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static BookPlatform.Common.ApplicationConstants;

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

        public async Task<ICollection<ApplicationUserViewModel>> GetFriendsAsync(string userId)
        {
            // check if user id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // find user
            ApplicationUser? user = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // generate empty model
            ICollection<ApplicationUserViewModel> model = new List<ApplicationUserViewModel>();

            // check if user has any friends
            if (!user.Friends.Any())
            {
                return model;
            }

            // populate model
            model = user.Friends                
                .OrderBy(u => u.UserName)
                .Select(u => new ApplicationUserViewModel()
                {
                    Email = u.Email!,
                    UserName = u.UserName!
                })
                .ToList();

            return model;
        }

        public async Task<ICollection<ApplicationUserViewModel>> FindFriendAsync(string userId, string friendEmail)
        {
            // generate empty model
            ICollection<ApplicationUserViewModel> model = await GetFriendsAsync(userId);                       

            // check if user id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                return model;
            }

            // find user
            ApplicationUser? user = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                throw new InvalidOperationException();
            }            

            // find user
            ApplicationUser? searchedUser = await this.userManager.FindByEmailAsync(friendEmail);

            if (searchedUser == null)
            {
                return model;
            }            

            // check if user is searching himself
            if (user == searchedUser)
            {
                return model;
            }            

            // check if user is in role User
            if (await userManager.IsInRoleAsync(searchedUser, UserRoleName))
            {
                // generate view model
                ApplicationUserViewModel searchedUserViewModel = new ApplicationUserViewModel()
                {
                    Email = searchedUser.Email!,
                    UserName = searchedUser.UserName!,
                };

                // add to list
                if (!model.Any(vm => vm.Email == searchedUser.Email))
                {
                    model.Add(searchedUserViewModel);
                }
            }            

            return model;
        }

        public async Task<bool> AddFriendAsync(string mainUserId, string friendEmail)
        {
            // check if user id is a valid guid
            Guid mainUserGuid = Guid.Empty;
            if (!IsGuidValid(mainUserId, ref mainUserGuid))
            {
                throw new ArgumentException();
            }

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                throw new ArgumentException();
            }

            // find users
            ApplicationUser? mainUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == mainUserGuid);

            ApplicationUser? friendUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Email == friendEmail);

            if (mainUser == null || friendUser == null || mainUser == friendUser)
            {
                return false;
            }

            // check if user is in role User
            if (!await userManager.IsInRoleAsync(mainUser, UserRoleName) ||
                !await userManager.IsInRoleAsync(friendUser, UserRoleName))
            {
                return false;
            }

            // check if users already in each other's friends lists
            if (mainUser.Friends.Any(fr => fr.Email == friendUser.Email) ||
                friendUser.Friends.Any(fr => fr.Email == mainUser.Email))
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
                throw new ArgumentException();
            }

            // check email
            if (String.IsNullOrWhiteSpace(friendEmail))
            {
                throw new ArgumentException();
            }

            // find users
            ApplicationUser? mainUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Id == mainUserGuid);

            ApplicationUser? friendUser = await this.userManager
                .Users
                .Include(u => u.Friends)
                .FirstOrDefaultAsync(u => u.Email == friendEmail);

            if (mainUser == null || friendUser == null || mainUser == friendUser)
            {
                return false;
            }

            // check if users already in each other's friends lists
            if (!mainUser.Friends.Any(fr => fr.Email == friendUser.Email) ||
                !friendUser.Friends.Any(fr => fr.Email == mainUser.Email))
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
