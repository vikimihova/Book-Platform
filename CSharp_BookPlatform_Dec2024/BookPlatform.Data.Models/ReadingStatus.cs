using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class ReadingStatus
    {
        [Key]
        [Comment("Primary key and numeric value of the status")]
        public int Id { get; set; }

        [Required]
        [Comment("Descriptive value of the status")]
        public string StatusDescription { get; set; } = null!;
    }
}
