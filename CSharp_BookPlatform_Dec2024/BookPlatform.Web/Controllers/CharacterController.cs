using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class CharacterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
