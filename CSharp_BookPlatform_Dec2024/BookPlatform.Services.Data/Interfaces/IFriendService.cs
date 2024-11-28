using BookPlatform.Core.ViewModels.ApplicationUser;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IFriendService
    {
        Task<IEnumerable<ApplicationUserViewModel>> GetFriendsAsync(string userId);

        Task<ApplicationUserViewModel?> FindFriendAsync(string friendUserName);

        Task<bool> AddFriendAsync(string mainUserId, string friendUserName);

        Task<bool> RemoveFriendAsync(string mainUserId, string friendUserName);
    }
}
