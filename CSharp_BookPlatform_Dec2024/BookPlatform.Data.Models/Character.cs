using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.CharacterValidationConstants;


namespace BookPlatform.Data.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(CharacterNameMaxLength)]
        [Comment("Full name of the character")]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsSubmittedByUser { get; set; } = false;

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<BookCharacter> CharacterBooks { get; set; } = new List<BookCharacter>();

        public ICollection<BookApplicationUser> CharacterBookApplicationUsers { get; set; } = new List<BookApplicationUser>();
    }
}
