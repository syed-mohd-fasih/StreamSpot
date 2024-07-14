using Microsoft.AspNetCore.Mvc;
using StreamSpotMVC.Data;
using StreamSpotMVC.Models;

namespace StreamSpotMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            return _context.Movies;
        }
    }
}
