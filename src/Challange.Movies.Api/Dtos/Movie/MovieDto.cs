using Challange.Movies.Api.Dtos.Showtime;

namespace Challange.Movies.Api.Dtos.Movie
{
    public class MovieDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImdbId { get; set; }

        public string Stars { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}
