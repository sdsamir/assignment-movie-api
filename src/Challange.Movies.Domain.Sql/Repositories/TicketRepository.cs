using Challange.Movies.Domain.Abstructions;
using Challange.Movies.Domain.Entities;
using Challange.Movies.Domain.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Challange.Movies.Domain.Sql.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly CinemaContext _context;

        public TicketRepository(CinemaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<Ticket> GetAsync(Guid id, CancellationToken cancel)
        {
            return _context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancel);
        }

        public async Task<IEnumerable<Ticket>> GetAsync(int showtimeId, CancellationToken cancel)
        {
            return await _context.Tickets
                .Where(x => x.ShowtimeId == showtimeId)
                .ToListAsync(cancel);
        }

        public async Task<Ticket> CreateAsync(Showtime showtime, IEnumerable<Seat> selectedSeats, CancellationToken cancel)
        {
            var ticket = _context.Tickets.Add(new Ticket
            {
                Showtime = showtime,
                Seats = new List<Seat>(selectedSeats)
            });

            await _context.SaveChangesAsync(cancel);

            return ticket.Entity;
        }

        public async Task<Ticket> CreateAsync(Ticket ticket)
        {
            var createdTicket = _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();
            return createdTicket.Entity;
        }

        public async Task<Ticket> ConfirmPaymentAsync(Ticket ticket, CancellationToken cancel)
        {
            ticket.Paid = true;
            _context.Update(ticket);
            await _context.SaveChangesAsync(cancel);
            return ticket;
        }
    } 
}
