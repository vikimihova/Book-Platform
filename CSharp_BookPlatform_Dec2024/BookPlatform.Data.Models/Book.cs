using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookPlatform.Data.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = null!;

        public int? PublicationYear { get; set; }

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
    }
}
