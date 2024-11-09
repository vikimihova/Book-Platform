using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Comment("Optional user rating")]
        public int? RatingId { get; set; }

        [ForeignKey(nameof(RatingId))]
        [Comment("Optional user rating")]
        public Rating? Rating { get; set; }

        [Comment("Optional review for this book by the user")]
        public Review? Review { get; set; }

        [Required]
        [Comment("Date on which the user added the book to a reading list")]
        public DateTime DateAdded { get; set; }

        [Comment("Date on which the user started reading")]
        public DateTime? DateStarted { get; set; }

        [Comment("Date on which the user finished reading")]
        public DateTime? DateFinished { get; set; }

        [Required]
        [Comment("Current reading status")]
        public int ReadingStatusId { get; set; }

        [Required]
        [ForeignKey(nameof(ReadingStatusId))]
        [Comment("Current reading status")]
        public ReadingStatus ReadingStatus { get; set; } = null!;

        [Comment("User's favourite character from the book - optional")]
        public Guid? CharacterId { get; set; }

        [ForeignKey(nameof(CharacterId))]
        [Comment("User's favourite character from the book - optional")]
        public Character? Character { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
    }
}
