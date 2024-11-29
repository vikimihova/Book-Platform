using Microsoft.AspNetCore.Mvc;

namespace BookPlatform.Web.Areas.Admin.Controllers
{
    public class BookManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
