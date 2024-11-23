namespace BookPlatform.Core.ViewModels.Book
{
    public class BookDetailsViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int PublicationYear { get; set; }

        public string Author { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Description { get; set; } = null!;

        public double AverageRating { get; set; }

        public string? ImageUrl { get; set; }
    }
}
