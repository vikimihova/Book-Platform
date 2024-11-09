using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.GenreValidationConstants;

namespace BookPlatform.Services.Data.DataProcessor.ImportDtos
{
    public class GenreImportDto
    {
        [Required]
        [MinLength(GenreNameMinLength)]
        [MaxLength(GenreNameMaxLength)]
        public string Genre { get; set; } = null!;
    }
}
