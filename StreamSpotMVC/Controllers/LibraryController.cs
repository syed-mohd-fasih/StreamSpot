using Microsoft.AspNetCore.Mvc;

namespace StreamSpotMVC.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
