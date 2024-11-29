using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    public class CharacterManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
