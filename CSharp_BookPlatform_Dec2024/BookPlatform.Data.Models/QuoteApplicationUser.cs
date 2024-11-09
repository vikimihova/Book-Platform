using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPlatform.Data.Models
{
    [PrimaryKey(nameof(QuoteId), nameof(ApplicationUserId))]
    public class QuoteApplicationUser
    {
        [Required]
        public Guid QuoteId { get; set; }

        [Required]
        [ForeignKey(nameof(QuoteId))]
        public Quote Quote { get; set; } = null!;

        [Required]
        public Guid ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
