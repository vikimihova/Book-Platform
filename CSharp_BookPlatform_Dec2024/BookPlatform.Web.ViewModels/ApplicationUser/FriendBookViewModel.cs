using System.Globalization;

namespace BookPlatform.Core.ViewModels.ApplicationUser
{
    public class FriendBookViewModel
    {
        public string FriendUserName { get; set; } = null!;

        public string BookId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string ReadingStatusDescription { get; set; } = null!;
    }
}
