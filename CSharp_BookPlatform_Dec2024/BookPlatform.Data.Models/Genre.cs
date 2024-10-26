using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;
    }
}
