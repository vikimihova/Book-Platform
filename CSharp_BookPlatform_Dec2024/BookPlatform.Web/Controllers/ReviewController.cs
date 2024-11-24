using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
