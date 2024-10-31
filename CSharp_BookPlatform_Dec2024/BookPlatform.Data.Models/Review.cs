using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPlatform.Data.Models
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Content { get; set; } = null!;
        
        [Required]
        public Guid BookId { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }

        [Required]
        public BookApplicationUser BookApplicationUser { get; set; } = null!;
    }
}
