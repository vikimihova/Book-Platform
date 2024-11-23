using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Data.Models;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountController(
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> MakeUserAdmin()
        {
            string roleName = "Admin";

            IdentityResult? roleToCreate = null;

            if (await roleManager.RoleExistsAsync(roleName) == false)
            {
                roleToCreate = await roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            if (!User.IsInRole(roleName) && (roleToCreate == null || roleToCreate.Succeeded))
            {
                var currentUser = await userManager.FindByNameAsync(User.Identity!.Name!);

                if (currentUser != null)
                {
                    await userManager.AddToRoleAsync(currentUser, roleName);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
