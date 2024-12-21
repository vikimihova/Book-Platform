using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }

        [Comment("Last login time")]
        public DateTime? LastLogin { get; set; }

        public ICollection<ApplicationUser> Friends { get; set; } = new List<ApplicationUser>();

        public ICollection<BookApplicationUser> UserBooks { get; set; } = new List<BookApplicationUser>();

        // To be implemented later
        //public ICollection<QuoteApplicationUser> UserQuotes { get; set; } = new List<QuoteApplicationUser>();
    }
}
