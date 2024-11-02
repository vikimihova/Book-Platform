using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.AuthorValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;

namespace BookPlatform.Services.Data.DataProcessor.ImportDtos
{
    public class BookImportDto
    {
        [Required]
        [MinLength(AuthorFirstNameMinLength + AuthorLastNameMinLength)]
        [MaxLength(AuthorFirstNameMaxLength + AuthorLastNameMaxLength)]
        public string Author { get; set; } = null!;
                
        public string? Country { get; set; }

        [Required]
        [MaxLength(MaxImageUrlLength/2)]
        public string ImageLink { get; set; } = null!;

        public string? Language { get; set; }

        [Required]
        [MaxLength(MaxImageUrlLength / 2)]
        public string Link { get; set; } = null!;
        
        public int? Pages { get; set; }

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public int Year { get; set; }
    }
}
