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
        private readonly IRepository<Book, Guid> bookRepository;

        public CharacterService(
            IRepository<Character, Guid> characterRepository,
            IRepository<BookCharacter, object> bookCharacterRepository,
            IRepository<Book, Guid> bookRepository)
        {
            this.characterRepository = characterRepository;
            this.bookCharacterRepository = bookCharacterRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<CharacterIndexViewModel>> GetCharactersIndexAsync(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            IEnumerable<CharacterIndexViewModel> characters = await this.bookCharacterRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(bc => bc.Character)
                .Where(bc => bc.BookId == bookGuid)
                .OrderBy(bc => bc.Character.Name)
                .Select(bc => new CharacterIndexViewModel()
                {
                    Id = bc.CharacterId.ToString(),
                    Name = bc.Character.Name,
                    BookId = bookId,
                    IsSubmittedByUser = bc.IsSubmittedByUser,
                    IsDeleted = bc.IsDeleted,
                })
                .ToListAsync();

            return characters;
        }

        public async Task<ICollection<SelectCharacterViewModel>> GetCharactersAsync(string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            ICollection<SelectCharacterViewModel> characters = await this.bookCharacterRepository
                .GetAllAttached()
                .AsNoTracking()
                .Include(bc => bc.Character)
                .Include(bc => bc.Book)
                .Where(bc => bc.Book.IsDeleted == false && 
                             bc.Book.Id == bookGuid &&
                             bc.Character.IsDeleted == false)
                .OrderBy(bc => bc.Character.Name)
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
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
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
                    CharacterId = characterGuid,
                    IsSubmittedByUser = true
                };

                await this.bookCharacterRepository.AddAsync(bookCharacter);

                return true;
            }
            else
            {
                return false;
            }            
        }

        public async Task<bool> AddCharacterByAdminAsync(AddCharacterInputModel model)
        {
            // check if bookId is valid
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(model.BookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
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
                    CharacterId = characterGuid,
                    IsSubmittedByUser = false
                };

                await this.bookCharacterRepository.AddAsync(bookCharacter);

                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> SoftDeleteCharacterAsync(string characterId, string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid characterGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) ||
                !IsGuidValid(characterId, ref characterGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }            

            // check if character exists
            Character? character = await this.characterRepository.GetByIdAsync(characterGuid);

            if (character == null)
            {
                throw new InvalidOperationException();
            }

            // check if bookCharacter exists
            BookCharacter? bookCharacter = await this.bookCharacterRepository
                .FirstOrDefaultAsync(bc => bc.CharacterId == characterGuid && bc.BookId == bookGuid);

            if (bookCharacter == null)
            {
                throw new InvalidOperationException();
            }

            // check if bookCharacter already deleted
            if (bookCharacter.IsDeleted == true)
            {
                return false;
            }

            // soft delete character
            bookCharacter.IsDeleted = true;
            await this.bookCharacterRepository.UpdateAsync(bookCharacter);

            return true;
        }

        public async Task<bool> IncludeCharacterAsync(string characterId, string bookId)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            Guid characterGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid) ||
                !IsGuidValid(characterId, ref characterGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null)
            {
                throw new InvalidOperationException();
            }

            // check if character exists
            Character? character = await this.characterRepository.GetByIdAsync(characterGuid);

            if (character == null)
            {
                throw new InvalidOperationException();
            }

            // check if bookCharacter exists
            BookCharacter? bookCharacter = await this.bookCharacterRepository
                .FirstOrDefaultAsync(bc => bc.CharacterId == characterGuid && bc.BookId == bookGuid);

            if (bookCharacter == null)
            {
                throw new InvalidOperationException();
            }

            // check if bookCharacter already deleted
            if (bookCharacter.IsDeleted == false)
            {
                return false;
            }

            // include character
            bookCharacter.IsDeleted = false;
            await this.bookCharacterRepository.UpdateAsync(bookCharacter);

            return true;
        }

        // AUXILIARY

        public async Task<AddCharacterInputModel> GenerateAddCharacterInputModelAsync(string bookId, int? readingStatusId = null)
        {
            // check input
            Guid bookGuid = Guid.Empty;
            if (!IsGuidValid(bookId, ref bookGuid))
            {
                throw new ArgumentException();
            }

            // check if book exists
            Book? book = await this.bookRepository.GetByIdAsync(bookGuid);

            if (book == null || book.IsDeleted == true)
            {
                throw new InvalidOperationException();
            }

            AddCharacterInputModel model = new AddCharacterInputModel();
            model.BookId = bookId;
            model.BookTitle = book.Title;
            model.ImageUrl = book.ImageUrl;

            if (readingStatusId != null)
            {
                model.ReadingStatusId = readingStatusId;
            }            

            return model;
        }
    }
}
