using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class QuoteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
