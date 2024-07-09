using StreamSpotMVC.Data;
using StreamSpotMVC.Models;
using System.Text.Json;

namespace StreamSpotMVC.Services
{
    public class MoviesDirectoryService
    {
        // Save directory name where movies are saved
        private readonly string MoviesDirectory;
        private readonly ApplicationDbContext _context;

        public MoviesDirectoryService(ApplicationDbContext context, string moviesDirectory = "D:\\Movies")
        {
            _context = context;
            MoviesDirectory = moviesDirectory;
        }

        // Get all movies
        // Assuming each sub directory has a .json file enclosing all data about the movie
        public IEnumerable<Movie> GetMovies()
        {
            if (!Directory.Exists(MoviesDirectory))
                return Enumerable.Empty<Movie>();

            var directories = Directory.GetDirectories(MoviesDirectory);

            var movies = new List<Movie>();

            foreach (var d in directories)
            {
                var jsonFilePath = Path.Combine(d, "movie.json");

                if (File.Exists(jsonFilePath))
                {
                    using (var jsonFileReader = File.OpenText(jsonFilePath))
                    {
                        var movie = JsonSerializer.Deserialize<Movie>(jsonFileReader.ReadToEnd(),
                            new JsonSerializerOptions
                            {
                                PropertyNameCaseInsensitive = true,
                                AllowTrailingCommas = true,
                            }
                        );
                        if (movie != null)
                        {
                            movies.Add(movie);
                        }
                    }
                }
            }

            if (movies.Count > 0)
            {
                return movies;
            }
            else
            {
                return Enumerable.Empty<Movie>();
            }
        }

        public static void InitializeDb(ApplicationDbContext context, string moviesDirectory = "D:\\Movies")
        {
            if (!context.Movies.Any())
            {
                var movies = new MoviesDirectoryService(context).GetMovies();
                context.Movies.AddRange(movies);
                context.SaveChanges();
            }
        }

        public void SyncMovies()
        {
            var directories = Directory.GetDirectories(MoviesDirectory);

            foreach(var directory in directories)
            {
                var jsonFilePath = Path.Combine(directory, "movie.json");

                if (File.Exists(jsonFilePath))
                {
                    var json = File.ReadAllText(jsonFilePath);
                    var movie = JsonSerializer.Deserialize<Movie>(json,
                        new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                            AllowTrailingCommas = true,
                        });
                    if(movie != null && !_context.Movies.Any(m => m.Title == movie.Title))
                    {
                        _context.Movies.Add(movie);
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
