namespace BookPlatform.Core.ViewModels.Character
{
    public class CharacterIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string BookId { get; set; } = null!;

        public bool IsSubmittedByUser { get; set; }

        public bool IsDeleted { get; set; }
    }
}
