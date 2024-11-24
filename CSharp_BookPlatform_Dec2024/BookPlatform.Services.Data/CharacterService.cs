using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Core.Services
{
    public class CharacterService : BaseService, ICharacterService
    {
        public CharacterService(
            UserManager<ApplicationUser> userManager) : base(userManager)
        {
            
        }
    }
}
