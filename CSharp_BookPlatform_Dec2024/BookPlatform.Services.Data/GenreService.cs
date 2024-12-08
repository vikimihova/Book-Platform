using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Core.ViewModels.Author;
using BookPlatform.Core.ViewModels.Genre;
using BookPlatform.Data.Models;
using BookPlatform.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Core.Services
{
    public class GenreService : BaseService, IGenreService
    {
        private readonly IRepository<Genre, Guid> genreRepository;

        public GenreService(IRepository<Genre, Guid> genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task<ICollection<SelectGenreViewModel>> GetGenresAsync()
        {
            ICollection<SelectGenreViewModel> genres = await this.genreRepository
                .GetAllAttached()
                .AsNoTracking()
                .Where(g => g.IsDeleted == false)
                .Select(g => new SelectGenreViewModel()
                {
                    Id = g.Id.ToString(),
                    Name = g.Name
                })
                .ToListAsync();

            return genres;
        }
    }
}
