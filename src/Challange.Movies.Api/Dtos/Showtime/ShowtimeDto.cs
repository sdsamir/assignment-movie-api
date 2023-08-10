using Challange.Movies.Api.Dtos.Auditorium;
using Challange.Movies.Api.Dtos.Movie;
using Challange.Movies.Api.Dtos.Ticket;
using System.ComponentModel.DataAnnotations;

namespace Challange.Movies.Api.Dtos.Showtime
{
    public class ShowtimeDto
    {
        public int Id { get; set; }

        public DateTime SessionDate { get; set; }

        public int AuditoriumId { get; set; }

        public MovieDto Movie { get; set; }

        public ICollection<TicketDto> Tickets { get; set; }
    }
}
