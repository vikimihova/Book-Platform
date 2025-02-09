using System.ComponentModel.DataAnnotations;

using static BookPlatform.Common.ApplicationConstants;
using static BookPlatform.Common.EntityValidationConstants.BookValidationConstants;
using static BookPlatform.Common.EntityValidationConstants.ReviewValidationConstants;

using BookPlatform.Core.ViewModels.Character;
using BookPlatform.Core.ViewModels.Rating;

namespace BookPlatform.Core.ViewModels.ReadingList
{
    public class ReadingListAddInputModel
    {
        [Required]
        public string BookId { get; set; } = null!;

        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string BookTitle { get; set; } = null!;

        [Range(1, 5)]
        public int? Rating { get; set; }

        public ICollection<SelectRatingViewModel> Ratings { get; set; } = new List<SelectRatingViewModel>();

        [Required]
        public int ReadingStatus { get; set; }

        public string? DateFinished { get; set; } = DateTime.Now.ToString(DateInputFormat);
                
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
