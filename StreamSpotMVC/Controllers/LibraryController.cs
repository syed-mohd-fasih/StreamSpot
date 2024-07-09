using Microsoft.AspNetCore.Mvc;
using StreamSpotMVC.Data;
using StreamSpotMVC.Models;
using StreamSpotMVC.Services;

namespace StreamSpotMVC.Controllers
{
    public class LibraryController : Controller
    {
        ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context) 
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();

            return View(movies);
        }
    }
}
