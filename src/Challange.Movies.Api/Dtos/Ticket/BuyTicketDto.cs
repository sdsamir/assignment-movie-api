using System.ComponentModel.DataAnnotations;

namespace Challange.Movies.Api.Dtos.Ticket
{
    public class BuyTicketDto
    {
        [Required]
        public Guid TicketId { get; set; }
    }
}