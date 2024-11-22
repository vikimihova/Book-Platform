namespace BookPlatform.Web.ViewModels.ReadingList
{
    public class ReadingListViewModel
    {
        public string BookId { get; set; } = null!;

        public string BookTitle { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int? Rating { get; set; }

        public string ReadingStatus { get; set; } = null!;

        public string? DateStarted { get; set; }

        public string? DateFinished { get; set; }

        public string DateAdded { get; set; } = null!;

        public string? FavoriteCharacter { get; set; }

        public string? Review { get; set; }

        public string ImageUrl { get; set; } = null!;
    }
}
