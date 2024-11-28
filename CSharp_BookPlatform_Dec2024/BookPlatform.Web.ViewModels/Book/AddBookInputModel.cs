using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using BookPlatform.Core.ViewModels.Author;
using BookPlatform.Core.ViewModels.Genre;

namespace BookPlatform.Core.ViewModels.Book
{
    public class AddBookInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int PublicationYear { get; set; }

        [Required]
        public string AuthorId { get; set; } = null!;

        [Required]
        public ICollection<SelectAuthorViewModel> Authors { get; set; } = new List<SelectAuthorViewModel>();

        [Required]
        public string GenreId { get; set; } = null!;

        [Required]
        public ICollection<SelectGenreViewModel> Genres { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(MaxImageUrlLength)]
        public string ImageUrl { get; set; } = NoImageUrl;
    }
}
