using Microsoft.EntityFrameworkCore;
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
        [Comment("First or only name of the author")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(AuthorLastNameMaxLength)]
        [Comment("Last or only name of the author")]
        public string LastName { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
