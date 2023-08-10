using Challange.Movies.Api.Dtos.Auditorium;

namespace Challange.Movies.Api.Dtos.Seat
{
    public class SeatDto
    {
        public short Row { get; set; }

        public short Col { get; set; }

        public string SeatNumber { get; set; }
    }
}
