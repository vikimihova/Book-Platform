using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public ICollection<ApplicationUser> Friends { get; set; } = new List<ApplicationUser>();

        public ICollection<BookApplicationUser> UserBooks { get; set; } = new List<BookApplicationUser>();

        public ICollection<QuoteApplicationUser> UserQuotes { get; set; } = new List<QuoteApplicationUser>();
    }
}
