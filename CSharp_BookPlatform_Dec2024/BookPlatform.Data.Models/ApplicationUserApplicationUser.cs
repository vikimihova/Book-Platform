using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookPlatform.Data.Models
{
    public class ApplicationUserApplicationUser
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public Guid FriendId { get; set; }

        [Required]
        [ForeignKey(nameof(FriendId))]
        public ApplicationUser Friend { get; set; } = null!;
    }
}