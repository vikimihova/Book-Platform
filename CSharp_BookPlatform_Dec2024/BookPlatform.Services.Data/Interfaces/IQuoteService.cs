using BookPlatform.Core.ViewModels.Quote;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteViewModel>> GetAllQuotesPerBookAsync(string bookId);
    }
}
