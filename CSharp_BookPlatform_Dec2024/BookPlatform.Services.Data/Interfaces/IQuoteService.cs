using BookPlatform.Core.ViewModels.Quote;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IQuoteService
    {
        Task<IEnumerable<QuoteViewModel>> GetAllQuotesPerBookAsync(string bookId);

        // To-Do: Task<IEnumerable<QuoteViewModel>> for getting all quotes saved by a specific user

        // To-Do: Task<bool> for adding quotes to Favorites

        // To-Do: Task<bool> for removing quotes from Favorites
    }
}
