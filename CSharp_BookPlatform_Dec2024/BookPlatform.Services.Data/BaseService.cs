using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

namespace BookPlatform.Core.Services
{
    public class BaseService : IBaseService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public BaseService(
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            bool isGuidValid = Guid.TryParse(id, out parsedGuid);
            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }

        public async Task<string?> GetReadingStatusAsync(string userId, string bookId, IReadingListService readingListService)
        {
            ReadingStatus? readingStatus = null;

            ApplicationUser? currentUser = await userManager.FindByIdAsync(userId);

            if (currentUser != null)
            {
                // invoke method from ReadingListService

                readingStatus = await readingListService.GetReadingStatusForCurrentBookApplicationUserAsync(bookId, currentUser.Id);
            }

            if (readingStatus != null)
            {
                return readingStatus.StatusDescription;
            }

            return null;
        }
    }
}
