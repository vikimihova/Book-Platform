using BookPlatform.Core.ViewModels.Genre;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IGenreService
    {
        Task<ICollection<SelectGenreViewModel>> GetGenresAsync();
    }
}
