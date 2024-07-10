using Microsoft.AspNetCore.Mvc;
using StreamSpotMVC.Data;
using StreamSpotMVC.Models;
using StreamSpotMVC.Services;

namespace StreamSpotMVC.Controllers
{
    public class LibraryController : Controller
    {
        ApplicationDbContext _context;
        private readonly string _videoDirectory = @"D:\Movies"; // Path to your video directory

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies.ToList();

            return View(movies);
        }

        public IActionResult Movie(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var movie = _context.Movies.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpGet("/library/video/{*fileName}")]
        public IActionResult Play(string fileName)
        {
            var filePath = Path.Combine(_videoDirectory, fileName);

            if (System.IO.File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "video/mp4"); // Adjust MIME type based on your video format
            }

            return NotFound();
        }
    }
}
