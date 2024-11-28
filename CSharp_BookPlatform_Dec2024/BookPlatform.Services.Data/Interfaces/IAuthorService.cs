using BookPlatform.Core.ViewModels.Author;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<ICollection<SelectAuthorViewModel>> GetAuthorsAsync();
    }
}
