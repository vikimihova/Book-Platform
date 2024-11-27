using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.ReviewValidationConstants;

namespace BookPlatform.Data.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(ReviewContentMaxLength)]
        [Comment("The body of the review")]
        public string Content { get; set; } = null!;

        [Required]
        [Comment("Date of creation in the database")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Comment("Date of last modification in the database")]
        public DateTime? ModifiedOn { get; set; }

        [Required]
        [Comment("The book the review is about")]
        public Guid BookId { get; set; }

        [Required]
        [Comment("The user who created the review")]
        public Guid ApplicationUserId { get; set; }

        [Required]
        public BookApplicationUser BookApplicationUser { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
