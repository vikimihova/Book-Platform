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
    [PrimaryKey(nameof(BookId), nameof(ApplicationUserId))]
    public class BookApplicationUser
    {
        [Required]
        public Guid BookId { get; set; }

        [Required]
        [ForeignKey(nameof(BookId))]
        public Book Book { get; set; } = null!;

        [Required]
        public Guid ApplicationUserId { get; set; }

        [Required]
        [ForeignKey(nameof(ApplicationUserId))]
        public ApplicationUser ApplicationUser { get; set; } = null!;

        public int Rating { get; set; }

        public Review? Review { get; set; }

        public DateTime? DateStarted { get; set; }

        public DateTime? DateFinished { get; set; }

        [Required]
        public ReadingStatus ReadingStatus { get; set; }
        
        public Guid? CharacterId { get; set; }

        [ForeignKey(nameof(CharacterId))]
        public Character? Character { get; set; }
    }
}
