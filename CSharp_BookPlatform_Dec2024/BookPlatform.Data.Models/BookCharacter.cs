using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPlatform.Data.Models
{
    [PrimaryKey(nameof(BookId), nameof(CharacterId))]
    public class BookCharacter
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;

        [Required]
        public Guid CharacterId { get; set; }

        [Required]
        [ForeignKey(nameof(CharacterId))]
        public Character Character { get; set; } = null!;

        [Required]
        public bool IsSubmittedByUser { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
