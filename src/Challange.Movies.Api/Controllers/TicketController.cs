using Challange.Movies.Api.Dtos.Ticket;
using Challange.Movies.Api.Services.Showtime;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    //[Route("[Challange.Movies.Api/controller]")]
    public class TicketController : Controller
    {
        private readonly IShowtimeService _showtimeService;

        public TicketController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService?? throw new ArgumentNullException(nameof(showtimeService));
        }

        [HttpGet("tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAsync(int showtimeId)
        {
            var tickets = await _showtimeService.Tickets(showtimeId);
            return Ok(tickets);
        }

        [HttpPost("book/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> BookAsync(CreateTicketDto createTicket)
        {
            var createdTicket = await _showtimeService.BookTicket(createTicket);
            return Ok(createdTicket);
        }

        [HttpPost("{id}/buy/tickets")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> BuyAsync(Guid id)
        {
            return Ok(id);
        }
    }
}
