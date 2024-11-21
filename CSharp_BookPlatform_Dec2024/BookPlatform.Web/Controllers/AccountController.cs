using BookPlatform.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
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
                roleToCreate = await this.roleManager.CreateAsync(new ApplicationRole(roleName));
            }

            if (!User.IsInRole(roleName) && (roleToCreate == null || roleToCreate.Succeeded))
            {
                var currentUser = await this.userManager.FindByNameAsync(this.User.Identity!.Name!);

                if (currentUser != null)
                {
                    await userManager.AddToRoleAsync(currentUser, roleName);
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
