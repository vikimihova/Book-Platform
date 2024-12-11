using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Admin.UserManagement;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Core.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }               

        public async Task<IEnumerable<AllApplicationUsersViewModel>> GetAllUsersAsync()
        {
            // get all users
            IEnumerable<ApplicationUser> allUsers = await this.userManager.Users
                .AsNoTracking()
                .Where(u => u.UserName != AdminRoleName)
                .ToArrayAsync();

            // populate view model
            ICollection<AllApplicationUsersViewModel> allUsersViewModel = new List<AllApplicationUsersViewModel>();

            foreach (ApplicationUser user in allUsers)
            {
                // get roles per user
                IEnumerable<string> roles = await this.userManager.GetRolesAsync(user);

                // map to view model
                allUsersViewModel.Add(new AllApplicationUsersViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Roles = roles
                });
            }

            return allUsersViewModel;
        }

        public async Task<bool> MakeAdminAsync(string userId)
        {
            // check if id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if user is already an admin
            bool userAlreadyAdmin = await this.userManager.IsInRoleAsync(user, AdminRoleName);

            if (userAlreadyAdmin)
            {
                throw new InvalidOperationException();
            }

            // make admin
            IdentityResult result = await userManager.AddToRoleAsync(user, AdminRoleName);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> RemoveAdminAsync(string userId)
        {
            // check if id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if user is already an admin
            bool userAlreadyAdmin = await this.userManager.IsInRoleAsync(user, AdminRoleName);

            if (!userAlreadyAdmin)
            {
                throw new InvalidOperationException();
            }

            // remove admin role
            IdentityResult result = await userManager.RemoveFromRoleAsync(user, AdminRoleName);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            // check if id is a valid guid
            Guid userGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid))
            {
                throw new ArgumentException();
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.Users
                .Include(u => u.Friends)
                .ThenInclude(f => f.Friends)
                .FirstOrDefaultAsync(u => u.Id == userGuid);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if user has friends
            if (user.Friends.Any())
            {
                List<string> friendEmails = new List<string>();

                foreach (var friend in user.Friends)
                {
                    friendEmails.Add(friend.Email!);     
                }

                foreach (var email in friendEmails)
                {
                    ApplicationUser? friendUser = await this.userManager.Users
                        .Include(u => u.Friends)
                        .FirstOrDefaultAsync(u => u.Email == email);

                    if (friendUser != null) 
                    {
                        friendUser.Friends.Remove(user);
                        user.Friends.Remove(friendUser);

                        await userManager.UpdateAsync(user);
                        await userManager.UpdateAsync(friendUser);
                    }                    
                }
            }

            // delete user
            IdentityResult result = await userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }
    }
}
