namespace BookPlatform.Core.ViewModels.Review
{
    public class ReviewViewModel
    {
        public string Id { get; set; } = null!;

        public string BookId { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public string AuthorEmail { get; set; } = null!;

        public string Content { get; set; } = null!;

        public bool IsModified { get; set; } = false;

        public int? Rating { get; set; }

        public string? FavoriteCharacter { get; set; }
    }
}
