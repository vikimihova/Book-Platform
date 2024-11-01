using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.AuthorValidationConstants;

namespace BookPlatform.Data.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(AuthorFirstNameMaxLength)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(AuthorLastNameMaxLength)]
        public string LastName { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
