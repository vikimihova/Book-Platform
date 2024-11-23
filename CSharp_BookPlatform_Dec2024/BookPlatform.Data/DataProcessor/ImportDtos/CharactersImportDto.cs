using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;

namespace BookPlatform.Data.DataProcessor.ImportDtos
{
    public class CharactersImportDto
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public ICollection<string> Characters { get; set; } = new List<string>();
    }
}
