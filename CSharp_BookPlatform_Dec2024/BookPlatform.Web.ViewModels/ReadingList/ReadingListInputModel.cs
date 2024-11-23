using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.ReviewValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.CharacterValidationConstants;

namespace BookPlatform.Core.ViewModels.ReadingList
{
    public class ReadingListInputModel
    {
        [Required]
        public string BookId { get; set; } = null!;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string BookTitle { get; set; } = null!;

        public int? Rating { get; set; }

        [Required]
        public int ReadingStatus { get; set; }

        public string? DateStarted { get; set; }

        public string? DateFinished { get; set; }

        [Required]
        public string DateAdded { get; set; } = null!;

        [MinLength(CharacterNameMinLength)]
        [MaxLength(CharacterNameMaxLength)]
        public string? FavoriteCharacter { get; set; }

        [MinLength(ReviewContentMinLength)]
        [MaxLength(ReviewContentMaxLength)]
        public string? Review { get; set; }
    }
}
