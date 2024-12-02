using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

using BookPlatform.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using BookPlatform.Data.Models;

namespace BookPlatform.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false) 
            {
                return RedirectToAction("Index", "Book");
            }

            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode == 404)
            {
                return this.View("PageNotFound");
            }

            if (statusCode == 400)
            {
                return this.View("BadRequest");
            }

            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
