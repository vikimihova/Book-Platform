namespace BookPlatform.Core.ViewModels.Admin.UserManagement
{
    public class AllApplicationUsersViewModel
    {
        public string Id { get; set; } = null!;

        public string? Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
