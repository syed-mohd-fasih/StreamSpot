using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace StreamSpotMVC.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public string Description { get; set; }
        [JsonPropertyName("img")]
        public string Image { get; set; }
        public double Rating { get; set; }

        public Movie()
        {
            Id = 0;
            Year = 0;
            Title = "";
            Tags = [];
            Description = "";
            Image = "";
            Rating = 0;
        }
    }
}
