namespace BookPlatform.Web.ViewModels.Book
{
    public class BookIndexViewModel
    {
        public string Id { get; set; } = null!;
        
        public string Title { get; set; } = null!;
                
        public string Author { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string? ImageUrl { get; set; }
        
        public double AverageRating { get; set; }
    }
}
