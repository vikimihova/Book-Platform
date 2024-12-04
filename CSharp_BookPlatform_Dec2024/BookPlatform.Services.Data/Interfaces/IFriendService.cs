using BookPlatform.Core.ViewModels.ApplicationUser;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<ApplicationUserViewModel>> GetFriendsAsync(string userId);

        Task<IEnumerable<ApplicationUserViewModel>> FindFriendAsync(string friendEmail);

        Task<bool> AddFriendAsync(string mainUserId, string friendEmail);

        Task<bool> RemoveFriendAsync(string mainUserId, string friendEmail);
    }
}
