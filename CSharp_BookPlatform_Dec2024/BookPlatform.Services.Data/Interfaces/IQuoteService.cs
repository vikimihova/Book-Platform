using BookPlatform.Core.ViewModels.Quote;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteViewModel>> GetAllQuotesPerBookAsync(string bookId);

        Task<IEnumerable<QuoteViewModel>> GetAllQuotesPerUserAsync(string userId);

        Task<bool> AddQuoteAsync(string userId, string quoteId);

        Task<bool> RemoveQuoteAsync(string userId, string quoteId);
    }
}
