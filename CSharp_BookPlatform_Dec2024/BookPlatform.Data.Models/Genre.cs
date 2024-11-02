using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.GenreValidationConstants;

namespace BookPlatform.Data.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(GenreNameMaxLength)]
        [Comment("Name of the genre")]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
