using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class DiscoverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
