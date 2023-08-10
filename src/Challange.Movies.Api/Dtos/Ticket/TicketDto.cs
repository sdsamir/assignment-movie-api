using Challange.Movies.Api.Dtos.Seat;
using Challange.Movies.Api.Dtos.Showtime;

namespace Challange.Movies.Api.Dtos.Ticket
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Paid { get; set; }
        public DateTime Showtime { get; set; }
        public int ShowtimeId { get; set; }

        public ICollection<string> Seats { get; set; }
    }
}
