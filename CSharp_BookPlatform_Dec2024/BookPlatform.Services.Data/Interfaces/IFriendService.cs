using BookPlatform.Core.ViewModels.ApplicationUser;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IFriendService
    {
        Task<ICollection<ApplicationUserViewModel>> GetFriendsAsync(string userId);

        Task<ICollection<ApplicationUserViewModel>> FindFriendAsync(string userId, string friendEmail);

        Task<bool> AddFriendAsync(string mainUserId, string friendEmail);

        Task<bool> RemoveFriendAsync(string mainUserId, string friendEmail);
    }
}
