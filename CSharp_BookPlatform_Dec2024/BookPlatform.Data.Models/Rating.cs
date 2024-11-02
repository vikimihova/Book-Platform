using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class Rating
    {
        [Key]
        [Comment("Primary key and numeric value of the rating")]
        public int Id { get; set; }
        
        [Required]
        [Comment("Descriptive value of the rating")]
        public string RatingDescription { get; set; } = null!;
    }
}
