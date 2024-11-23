using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.EntityValidationConstants.AuthorValidationConstants;

namespace BookPlatform.Data.DataProcessor.ImportDtos
{
    public class AuthorImportDto
    {
        [Required]
        [MinLength(AuthorFirstNameMinLength + AuthorLastNameMinLength)]
        [MaxLength(AuthorFirstNameMaxLength + AuthorLastNameMaxLength)]
        public string Author { get; set; } = null!;
    }
}
