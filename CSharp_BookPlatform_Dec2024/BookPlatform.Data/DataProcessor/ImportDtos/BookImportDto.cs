using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.EntityValidationConstants.AuthorValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.GenreValidationConstants;

namespace BookPlatform.Data.DataProcessor.ImportDtos
{
    public class BookImportDto
    {
        [Required]
        [MinLength(AuthorFirstNameMinLength + AuthorLastNameMinLength)]
        [MaxLength(AuthorFirstNameMaxLength + AuthorLastNameMaxLength)]
        public string Author { get; set; } = null!;
        
        [Required]
        [MaxLength(MaxImageUrlLength)]
        public string ImageLink { get; set; } = NoImageUrl;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required]
        [MinLength(GenreNameMinLength)]
        [MaxLength(GenreNameMaxLength)]
        public string Genre { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;
    }
}
