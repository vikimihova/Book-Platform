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
        public string Name { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
