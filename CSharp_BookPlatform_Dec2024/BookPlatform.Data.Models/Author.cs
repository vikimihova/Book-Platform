using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        public string LastName { get; set; } = null!;

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
