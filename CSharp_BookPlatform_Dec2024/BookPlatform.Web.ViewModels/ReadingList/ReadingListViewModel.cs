namespace BookPlatform.Core.ViewModels.ReadingList
{
    public class ReadingListViewModel
    {
        public string BookId { get; set; } = null!;

        public string BookTitle { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int? Rating { get; set; }

        public int ReadingStatusId { get; set; }

        public string ReadingStatus { get; set; } = null!;

        public string DateFinished { get; set; } = null!;

        public string DateAdded { get; set; } = null!;

        public string? FavoriteCharacter { get; set; }

        public string? Review { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
