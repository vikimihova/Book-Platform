using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        public ICollection<ApplicationUserApplicationUser> UserFriends { get; set; } = new List<ApplicationUserApplicationUser>();

        public ICollection<BookApplicationUser> UserBooks { get; set; } = new List<BookApplicationUser>();

        public ICollection<QuoteApplicationUser> UserQuotes { get; set; } = new List<QuoteApplicationUser>();
    }
}
