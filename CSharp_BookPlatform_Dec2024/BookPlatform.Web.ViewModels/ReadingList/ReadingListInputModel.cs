using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.ReviewValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.CharacterValidationConstants;
using BookPlatform.Core.ViewModels.Character;

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

        [Range(1, 5)]
        public int? Rating { get; set; }

        [Required]
        public int ReadingStatus { get; set; }

        public string? DateStarted { get; set; }

        public string? DateFinished { get; set; }
                
        public string? CharacterId { get; set; }

        public ICollection<SelectCharacterViewModel> Characters { get; set; } = new List<SelectCharacterViewModel>();

        [MinLength(ReviewContentMinLength)]
        [MaxLength(ReviewContentMaxLength)]
        public string? Review { get; set; }

        [Required]
        [MaxLength(MaxImageUrlLength)]
        public string ImageUrl { get; set; } = NoImageUrl;
    }
}
