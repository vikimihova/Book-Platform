using BookPlatform.Core.ViewModels.Book;

namespace BookPlatform.Core.ViewModels.Quote
{
    public class AllBookQuotesViewModel
    {
        public BookDetailsViewModel BookDetails { get; set; } = null!;

        public IEnumerable<QuoteViewModel> Quotes { get; set; } = new List<QuoteViewModel>();
    }
}
