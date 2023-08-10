using System.ComponentModel.DataAnnotations;

namespace Challange.Movies.Api.Dtos.Ticket
{
    public class CreateTicketDto
    {
        [Required]
        public int ShowtimeId { get; set; }

        [Required]
        public int SeatCount { get; set; }
    }
}
