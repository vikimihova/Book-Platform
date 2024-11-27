using BookPlatform.Core.Services.Interfaces;
using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BookPlatform.Core.Services
{
    public class BaseService : IBaseService
    {
        public BaseService()
        {
        }

        public bool IsGuidValid(string? id, ref Guid parsedGuid)
        {
            // check string
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            // try parse string to guid
            bool isGuidValid = Guid.TryParse(id, out parsedGuid);

            if (!isGuidValid)
            {
                return false;
            }

            return true;
        }    
    }
}
