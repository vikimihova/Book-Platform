using Microsoft.EntityFrameworkCore;

using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Author;

using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;

namespace BookPlatform.Core.Services
{
    public class AuthorService : BaseService, IAuthorService
    {
        private readonly IRepository<Author, Guid> authorRepository;

        public AuthorService(IRepository<Author, Guid> authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<ICollection<SelectAuthorViewModel>> GetAuthorsAsync()
        {
            ICollection<SelectAuthorViewModel> authors = await this.authorRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(a => a.IsDeleted == false)
                .Select(a => new SelectAuthorViewModel()
                {
                    Id = a.Id.ToString(),
                    FullName = a.FullName
                })
                .ToListAsync();

            return authors;
        }
    }
}
