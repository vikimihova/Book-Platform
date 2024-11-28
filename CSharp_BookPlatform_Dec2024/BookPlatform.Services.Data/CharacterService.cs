using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Character;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
                .Where(bc => bc.Character.IsDeleted == false)
                .Where(bc => bc.BookId.ToString().ToLower() == bookId.ToLower())
                .Select(bc => new SelectCharacterViewModel()
                {
                    Id = bc.CharacterId.ToString(),
                    Name = bc.Character.Name
                })
                .ToListAsync();

            return characters;
        }

        public async Task<bool> AddCharacterAsync(AddCharacterInputModel model)
        {
            // check if bookId is valid
            Guid bookGuid = Guid.Empty;

            if (!IsGuidValid(model.BookId, ref bookGuid))
            {
                return false;
            }

            // check if character already exists, then add
            Character? character = await this.characterRepository
                .FirstOrDefaultAsync(c => c.Name == model.Name);            

            if (character == null)
            {
                character = new Character()
                {
                    Name = model.Name,
                    IsSubmittedByUser = true
                };

                await this.characterRepository.AddAsync(character);
            }

            Guid characterGuid = character.Id;

            // check if bookCharacter already exists, then add
            BookCharacter? bookCharacter = await this.bookCharacterRepository
                .FirstOrDefaultAsync(bc => bc.CharacterId == characterGuid 
                                        && bc.BookId == bookGuid);

            if (bookCharacter == null)
            {
                bookCharacter = new BookCharacter()
                {
                    BookId = bookGuid,
                    CharacterId = characterGuid
                };

                await this.bookCharacterRepository.AddAsync(bookCharacter);

                return true;
            }
            else
            {
                return false;
            }            
        }

        public async Task<bool> SoftDeleteCharacterAsync(string characterId)
        {
            // check if characterId is valid guid
            Guid characterGuid = Guid.Empty;

            if (!IsGuidValid(characterId, ref characterGuid))
            {
                return false;
            }

            // check if character exists
            Character? character = await this.characterRepository
                .GetByIdAsync(characterGuid);

            if (character == null)
            {
                return false;
            }

            // check if character already deleted
            if (character.IsDeleted == true)
            {
                return false;
            }

            // soft delete character
            character.IsDeleted = true;
            await this.characterRepository.UpdateAsync(character);

            return true;
        }
    }
}
