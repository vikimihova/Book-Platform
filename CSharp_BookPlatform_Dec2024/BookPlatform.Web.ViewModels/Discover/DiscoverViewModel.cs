using BookPlatform.Core.ViewModels.ApplicationUser;
using BookPlatform.Core.ViewModels.Review;

namespace BookPlatform.Core.ViewModels.Discover
{
    public class DiscoverViewModel
    {
        public IEnumerable<ReviewViewModel> NewReviews { get; set; } = new List<ReviewViewModel>();

        public ICollection<FriendBookViewModel> FriendBooks { get; set; } = new List<FriendBookViewModel>();
    }
}
