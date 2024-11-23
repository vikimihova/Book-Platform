using System.Security.Claims;

namespace BookPlatform.Core.Services.Interfaces
{
    public interface IBaseService
    {
        bool IsGuidValid(string? id, ref Guid parsedGuid);

        Task<string?> GetReadingStatusAsync(string userId, string bookId, IReadingListService readingListService);
    }
}
