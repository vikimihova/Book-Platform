using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookPlatform.Common.EntityValidationConstants.QuoteValidationConstants;

namespace BookPlatform.Data.Models
{
    public class Quote
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(QuoteContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public Guid CreatorId { get; set; }

        [Required]
        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser Creator { get; set; } = null!;

        [Required]
        public Guid BookId { get; set; }

        [Required]
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<QuoteApplicationUser> QuoteApplicationUsers { get; set; } = new List<QuoteApplicationUser>();
    }
}
