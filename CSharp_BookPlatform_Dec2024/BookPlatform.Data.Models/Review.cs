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
        public string Content { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }

        [Required]
        public BookApplicationUser BookApplicationUser { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
