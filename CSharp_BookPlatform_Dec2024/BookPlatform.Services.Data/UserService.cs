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
            IEnumerable<ApplicationUser> allUsers = await this.userManager.Users.ToArrayAsync();

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
                return false;
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            // check if user is already an admin
            bool userAlreadyAdmin = await this.userManager.IsInRoleAsync(user, AdminRoleName);

            if (userAlreadyAdmin)
            {
                return false;
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
                return false;
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
            }

            // check if user is already an admin
            bool userAlreadyAdmin = await this.userManager.IsInRoleAsync(user, AdminRoleName);

            if (!userAlreadyAdmin)
            {
                return false;
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
                return false;
            }

            // check if user exists
            ApplicationUser? user = await this.userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return false;
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
