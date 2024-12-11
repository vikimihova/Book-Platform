using BookPlatform.Core.ViewModels.Genre;

namespace BookPlatform.Core.ViewModels.Book
{
    public class BookIndexViewModelWrapper
    {
        public IEnumerable<BookIndexViewModel>? Books { get; set; }

        public string? SearchInput { get; set; }

        public string? GenreFilter { get; set; }

        public IEnumerable<SelectGenreViewModel>? Genres { get; set; }
    }
}
