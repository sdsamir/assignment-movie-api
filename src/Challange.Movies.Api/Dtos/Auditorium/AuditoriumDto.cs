using Challange.Movies.Api.Dtos.Seat;
using Challange.Movies.Api.Dtos.Showtime;

namespace Challange.Movies.Api.Dtos.Auditorium
{
    public class AuditoriumDto
    {
        public int Id { get; set; }

        public ICollection<ShowtimeDto> Showtimes { get; set; }

        public ICollection<string> Seats { get; set; }

        public int SeatCount { get; set; }
    }
}
