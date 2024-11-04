using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.CharacterValidationConstants;

namespace BookPlatform.Services.Data.DataProcessor.ImportDtos
{
    public class CharacterImportDto
    {
        [Required]
        [MinLength(CharacterNameMinLength)]
        [MaxLength(CharacterNameMaxLength)]
        public string Name { get; set; } = null!;
    }
}
