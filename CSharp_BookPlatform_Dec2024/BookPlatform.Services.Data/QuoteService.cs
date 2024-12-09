using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Quote;
using BookPlatform.Core.ViewModels.Review;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Core.Services
{
    public class QuoteService : BaseService, IQuoteService
    {
        private readonly IRepository<Quote, Guid> quoteRepository;
        private readonly IRepository<Book, Guid> bookRepository;

        public QuoteService(
            IRepository<Quote, Guid> quoteRepository,
            IRepository<Book, Guid> bookRepository)
        {
            this.quoteRepository = quoteRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<QuoteViewModel>> GetAllQuotesPerBookAsync(string bookId)
        {
            // check if bookId is a valid guid
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // find book
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            // generate quote view models
            IEnumerable<QuoteViewModel> quotes = await this.quoteRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(q => q.Book)
                .Where(q => q.BookId == bookGuid &&
                            q.Book.IsDeleted == false &&
                            q.IsDeleted == false)
                .Select(q => new QuoteViewModel()
                {                    
                    Content = q.Content
                })
                .ToListAsync();

            return quotes;
        }
    }
}
