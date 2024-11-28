using BookPlatform.Core.ViewModels.Character;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface ICharacterService
    {
        Task<ICollection<SelectCharacterViewModel>> GetCharactersAsync(string bookId);

        Task<bool> AddCharacterAsync(AddCharacterInputModel model);
    }
}
