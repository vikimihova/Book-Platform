using Microsoft.EntityFrameworkCore;
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
        [Comment("The body of the quote")]
        public string Content { get; set; } = null!;

        [Required]
        [Comment("Date of creation in the database")]
        public DateTime CreatedOn { get; set; } 

        [Required]
        [Comment("The book the quote is from")]
        public Guid BookId { get; set; }

        [Required]
        [ForeignKey(nameof(BookId))]
        [Comment("The book the quote is from")]
        public Book Book { get; set; } = null!;        

        [Required]
        public bool IsDeleted { get; set; } = false;

        // To be implemented later
        //public ICollection<QuoteApplicationUser> QuoteApplicationUsers { get; set; } = new List<QuoteApplicationUser>();
    }
}
