using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Core.Services
{
    public class QuoteService : BaseService, IQuoteService
    {
        public QuoteService(
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            
        }
    }
}
