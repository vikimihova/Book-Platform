using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Data.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Full title of the book")]
        public string Title { get; set; } = null!;

        [Required]
        [Comment("Official known first publication year of the book")]
        public int PublicationYear { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; } = null!;

        [Required]
        public Guid GenreId { get; set; }

        [Required]
        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;

        [Required]
        [Comment("Book description")]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(MaxImageUrlLength)]
        [Comment("Book cover image")]
        public string ImageUrl { get; set; } = NoImageUrl;

        [Required]
        [Comment("Average rating based on users' ratings")]
        public double AverageRating { get; set; } = 0.00;        

        [Required]
        public bool IsDeleted { get; set; } = false;

        public ICollection<BookApplicationUser> BookApplicationUsers { get; set; } = new List<BookApplicationUser>();

        public ICollection<BookCharacter> BookCharacters { get; set; } = new List<BookCharacter>();

        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
