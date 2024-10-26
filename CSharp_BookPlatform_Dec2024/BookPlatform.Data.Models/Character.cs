
using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<BookCharacter> CharacterBooks { get; set; } = new List<BookCharacter>();

        public ICollection<BookApplicationUser> CharacterBookApplicationUsers { get; set; } = new List<BookApplicationUser>();
    }
}
