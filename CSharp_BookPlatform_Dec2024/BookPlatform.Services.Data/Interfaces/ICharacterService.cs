using BookPlatform.Core.ViewModels.Character;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<IEnumerable<CharacterIndexViewModel>> GetCharactersIndexAsync(string bookId);

        Task<ICollection<SelectCharacterViewModel>> GetCharactersAsync(string bookId);

        Task<bool> AddCharacterAsync(AddCharacterInputModel model);

        Task<bool> AddCharacterByAdminAsync(AddCharacterInputModel model);

        Task<bool> SoftDeleteCharacterAsync(string characterId, string bookId);

        Task<bool> IncludeCharacterAsync(string characterId, string bookId);

        Task<AddCharacterInputModel> GenerateAddCharacterInputModelAsync(string bookId, int? readingStatusId = null);
    }
}
