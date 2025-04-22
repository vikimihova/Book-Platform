namespace BookPlatform.Core.ViewModels.ReadingList
{
    public class ReadingListPaginatedViewModel
    {
        public IEnumerable<ListedBookViewModel> Books { get; set; } = new List<ListedBookViewModel>();

        public int? totalBooksPerUserCount { get; set; }

        public int? CurrentPage { get; set; } = 1;

        public int? EntitiesPerPage { get; set; } = 4;

        public int? TotalPages { get; set; }
    }
}
