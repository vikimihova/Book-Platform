
using System.ComponentModel.DataAnnotations;

namespace BookPlatform.Data.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; } = null!;

    }
}
