using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Core.ViewModels.Review
{
    public class AllBookReviewsViewModel
    {
        public BookDetailsViewModel BookDetails { get; set; } = null!;

        public IEnumerable<ReviewViewModel> Reviews { get; set; } = new List<ReviewViewModel>();
    }
}
