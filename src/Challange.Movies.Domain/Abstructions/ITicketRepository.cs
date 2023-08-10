using Challange.Movies.Domain.Entities;
using System.Linq;

namespace Challange.Movies.Domain.Abstructions
{
    public interface ITicketRepository
    {
        Task<Ticket> ConfirmPaymentAsync(Ticket ticket, CancellationToken cancel);

        Task<Ticket> CreateAsync(Ticket ticket);

        Task<Ticket> CreateAsync(Showtime showtime, IEnumerable<Seat> selectedSeats, CancellationToken cancel);

        Task<Ticket> GetAsync(Guid id, CancellationToken cancel);

        Task<IEnumerable<Ticket>> GetAsync(int showtimeId, CancellationToken cancel);
    }
}
