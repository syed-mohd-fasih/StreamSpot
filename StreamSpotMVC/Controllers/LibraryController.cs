using Microsoft.AspNetCore.Mvc;
using StreamSpotMVC.Data;
using StreamSpotMVC.Services;

namespace StreamSpotMVC.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly string _videoDirectory = @"D:\Movies";
        private readonly MoviesDirectoryService _moviesDirectoryService;
        public static readonly Dictionary<string, string> MimeTypes = new Dictionary<string, string>
        {
            { ".mp4", "video/mp4" },
            { ".webm", "video/webm" },
            { ".ogv", "video/ogg" },
            { ".avi", "video/x-msvideo" },
            { ".mov", "video/quicktime" },
            { ".wmv", "video/x-ms-wmv" },
            { ".flv", "video/x-flv" },
            { ".mkv", "video/x-matroska" },
            { ".mpeg", "video/mpeg" }
        };

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Movies.ToList());
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
                var fileExtension = Path.GetExtension(filePath);
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, MimeTypes[fileExtension.ToLowerInvariant()]);
            }

            return NotFound();
        }

        // Only Search by title
        public IActionResult SearchResult(string SearchTitle)
        {
            return View("Index", _context.Movies.Where(m => m.Title.Contains(SearchTitle)).ToList());
        }
    }
}
