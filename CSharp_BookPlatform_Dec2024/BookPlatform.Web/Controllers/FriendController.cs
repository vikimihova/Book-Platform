using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Controllers
{
    public class FriendController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
