using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
