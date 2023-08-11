using Challange.Movies.Api.Dtos.Ticket;
using Challange.Movies.Api.Services.Showtime;
using Challange.Movies.Api.Services.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    //[Route("[Challange.Movies.Api/controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketRepository;

        public TicketController(ITicketService showtimeService)
        {
            _ticketRepository = showtimeService?? throw new ArgumentNullException(nameof(showtimeService));
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAsync(int showtimeId)
        {
            var tickets = await _ticketRepository.Tickets(showtimeId);
            return Ok(tickets);
        }

        [HttpPost("book/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> BookAsync([FromBody]CreateTicketDto createTicket)
        {
            var createdTicket = await _ticketRepository.BookTicket(createTicket);
            return Ok(createdTicket);
        }

        [HttpPost("{id}/buy/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> BuyAsync([FromBody]Guid id)
        {
            var UpdatedTicket = await _ticketRepository.BuyTicket(id);
            return Ok(UpdatedTicket);
        }
    }
}
