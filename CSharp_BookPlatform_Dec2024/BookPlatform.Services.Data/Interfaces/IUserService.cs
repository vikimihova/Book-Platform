using BookPlatform.Core.ViewModels.Admin.UserManagement;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<AllApplicationUsersViewModel>> GetAllUsersAsync();

        Task<bool> MakeAdminAsync(string userId);

        Task<bool> RemoveAdminAsync(string userId);

        Task<bool> DeleteUserAsync(string userId);
    }
}
