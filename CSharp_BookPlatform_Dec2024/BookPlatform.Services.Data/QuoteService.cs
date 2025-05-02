using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Quote;
using BookPlatform.Core.ViewModels.Review;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookPlatform.Core.Services
{
    public class QuoteService : BaseService, IQuoteService
    {
        private readonly IRepository<Quote, Guid> quoteRepository;
        private readonly IRepository<Book, Guid> bookRepository;
        private readonly IRepository<QuoteApplicationUser, object> quoteApplicationUserRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public QuoteService(
            IRepository<Quote, Guid> quoteRepository,
            IRepository<Book, Guid> bookRepository,
            IRepository<QuoteApplicationUser, object> quoteApplicationUserRepository,
            UserManager<ApplicationUser> userManager)
        {
            this.quoteRepository = quoteRepository;
            this.bookRepository = bookRepository;
            this.quoteApplicationUserRepository = quoteApplicationUserRepository;
            this.userManager = userManager;
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

        // To-Do: Task<IEnumerable<QuoteViewModel>> implementation for getting all quotes saved by a specific user
                
        public async Task<bool> AddQuoteAsync(string userId, string quoteId)
        {
            // check input
            Guid userGuid = Guid.Empty;
            Guid quoteGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid) ||
                !IsGuidValid(quoteId, ref quoteGuid))
            {
                throw new ArgumentException();
            }

            // check if quote exists
            Quote quote = await quoteRepository.GetByIdAsync(quoteGuid);

            if (quote == null)
            {
                throw new InvalidOperationException();
            }

            // check if user exists
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if QuoteApplicationUser exists            
            QuoteApplicationUser? quoteApplicationUser = await quoteApplicationUserRepository
                .FirstOrDefaultAsync(qau => qau.QuoteId == quoteGuid && qau.ApplicationUserId == userGuid);

            if (quoteApplicationUser == null)
            {
                // create new QuoteApplicationUser
                quoteApplicationUser = new QuoteApplicationUser()
                {
                    QuoteId = quoteGuid,
                    ApplicationUserId = userGuid
                };

                // add to dbSet and save Changes
                await quoteApplicationUserRepository.AddAsync(quoteApplicationUser);

                return true;
            }

            return false;
        }

        public async Task<bool> RemoveQuoteAsync(string userId, string quoteId)
        {
            // check input
            Guid userGuid = Guid.Empty;
            Guid quoteGuid = Guid.Empty;
            if (!IsGuidValid(userId, ref userGuid) ||
                !IsGuidValid(quoteId, ref quoteGuid))
            {
                throw new ArgumentException();
            }

            // check if quote exists
            Quote quote = await quoteRepository.GetByIdAsync(quoteGuid);

            if (quote == null)
            {
                throw new InvalidOperationException();
            }

            // check if user exists
            ApplicationUser? user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidOperationException();
            }

            // check if quoteApplicationUser exists
            QuoteApplicationUser? quoteApplicationUser = await quoteApplicationUserRepository
                .FirstOrDefaultAsync(qau => qau.QuoteId == quoteGuid && qau.ApplicationUserId == userGuid);

            // remove quote
            if (quoteApplicationUser != null)
            {
                await this.quoteApplicationUserRepository.DeleteAsync(quoteApplicationUser);

                return true;
            }

            return false;
        }
    }
}
