using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Core.Services
{
    public class CharacterService : BaseService, ICharacterService
    {
        private readonly IRepository<Character, Guid> characterRepository;
        private readonly IRepository<BookCharacter, object> bookCharacterRepository;

        public CharacterService(
            IRepository<Character, Guid> characterRepository,
            IRepository<BookCharacter, object> bookCharacterRepository)
        {
            this.characterRepository = characterRepository;
            this.bookCharacterRepository = bookCharacterRepository;
        }

        public async Task<ICollection<SelectCharacterViewModel>> GetCharactersAsync(string bookId)
        {
            ICollection<SelectCharacterViewModel> characters = await this.bookCharacterRepository
                .GetAllAttached()
                .Include(bc => bc.Character)
                .Where(bc => bc.BookId.ToString().ToLower() == bookId.ToLower())
                .Select(bc => new SelectCharacterViewModel()
                {
                    Id = bc.CharacterId.ToString(),
                    Name = bc.Character.Name
                })
                .ToListAsync();

            return characters;
        }
    }
}
