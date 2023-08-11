using Challange.Movies.Api.Dtos.Ticket;

namespace Challange.Movies.Api.Services.Ticket
{
    public interface ITicketService
    {
        Task<TicketDto> BookTicket(CreateTicketDto createTicket);

        Task<TicketDto> BuyTicket(BuyTicketDto buyTicket);

        Task<IEnumerable<TicketDto>> Tickets(int showtimeId);
    }
}
