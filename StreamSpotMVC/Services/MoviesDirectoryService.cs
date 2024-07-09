using StreamSpotMVC.Models;
using System.Text.Json;

namespace StreamSpotMVC.Services
{
    public class MoviesDirectoryService
    {
        // Save directory name where movies are saved
        private string? MoviesDirectoryName => "D:\\Movies";

        // Get all movies
        // Assuming each sub directory has a .json file enclosing all data about the movie
        public IEnumerable<Movie> GetMovies()
        {
            if (!Directory.Exists(MoviesDirectoryName))
                return Enumerable.Empty<Movie>();

            var directories = Directory.GetDirectories(MoviesDirectoryName);

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

            return movies;
        }
    }
}
