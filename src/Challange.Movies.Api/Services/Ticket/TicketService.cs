using Challange.Movies.Api.Dtos.Ticket;
using Challange.Movies.Domain.Abstructions;

namespace Challange.Movies.Api.Services.Ticket
{
    public class TicketService:ITicketService
    {

        private readonly IShowtimeRepository _showtimeRepository;
        private readonly ITicketRepository _ticketRepository;


        public TicketService(IShowtimeRepository showtimeRepository, ITicketRepository ticketRepository)
        {
            _showtimeRepository = showtimeRepository ?? throw new ArgumentNullException(nameof(showtimeRepository));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));

        }

        public async Task<IEnumerable<TicketDto>> Tickets(int showtimeId)
        {
            var ticketDBEntities = await _ticketRepository.GetAsync(showtimeId, CancellationToken.None);
            var tickets = ticketDBEntities
            .Select(x =>
                new TicketDto()
                {
                    Id = x.Id,
                    CreatedTime = x.CreatedTime,
                    Paid = x.Paid,
                    Seats = (x.Paid || x.CreatedTime >= DateTime.Now.AddMinutes(-3)) ? x.Seats.Select(x => x.SeatNumber).ToList() : new List<string>(),
                    Showtime = x.Showtime.SessionDate,
                    ShowtimeId = x.ShowtimeId
                }
            );

            return tickets;
        }

        public async Task<TicketDto> BookTicket(CreateTicketDto createTicket)
        {
            Domain.Entities.Ticket ticket = new Domain.Entities.Ticket();

            var showtime = await _showtimeRepository.GetAsync(createTicket.ShowtimeId, CancellationToken.None);
            var totalSeats = showtime.Auditorium.Seats.OrderBy(x => x.Id).ToList();

            var bookedSeats = showtime
            .Tickets
            .Where(t => t.Paid || t.CreatedTime >= DateTime.Now.AddMinutes(-3))
            .Select(x => x.Seats)
            .ToList();

            var autoCancelledSeats = showtime
            .Tickets.Where(t => !t.Paid && t.CreatedTime < DateTime.Now.AddMinutes(-3))
            .Select(x => x.Seats)
            .ToList();

            var assignedSeats = autoCancelledSeats
                                .Where(x => x.Count >= createTicket.SeatCount)
                                .OrderBy(x => x.Count)
                                .FirstOrDefault()
                                ?.Take(createTicket.SeatCount)
                                .ToList();


            var lastBookedSeat = bookedSeats?.LastOrDefault()?.LastOrDefault();

            if (assignedSeats == null && lastBookedSeat?.Id + createTicket.SeatCount > totalSeats.Count())
            {
                throw new Exception($"{createTicket.SeatCount} Tickets are not available.");
            }


            assignedSeats = assignedSeats ?? totalSeats
                                            .Skip(showtime.Tickets.Sum(x => x.Seats.Count()))
                                            .Take(createTicket.SeatCount).ToList();

            ticket.ShowtimeId = createTicket.ShowtimeId;
            ticket.Seats = assignedSeats;
            ticket.Paid = false;
            ticket.CreatedTime = DateTime.Now;

            var createdTicket = await _ticketRepository.CreateAsync(ticket);

            return new TicketDto()
            {
                Id = createdTicket.Id,
                CreatedTime = createdTicket.CreatedTime,
                Paid = createdTicket.Paid,
                Seats = createdTicket.Seats.Select(x => x.SeatNumber).ToList(),
                Showtime = showtime.SessionDate,
                ShowtimeId = showtime.Id
            };
        }

        public async Task<TicketDto> BuyTicket(Guid ticketId)
        {
            var ticketDBEntiry = await _ticketRepository.GetAsync(ticketId, CancellationToken.None);
            if(ticketDBEntiry == null)
            {
                throw new Exception("Requested ticket does not exist");
            }

            if (!ticketDBEntiry.Paid && ticketDBEntiry.CreatedTime < DateTime.Now.AddMinutes(-3))
            {
                throw new Exception("Requested ticket is already expired, Book new ticket");
            }

            if (ticketDBEntiry.Paid)
            {
                throw new Exception("Requested ticket is already paid");
            }

            ticketDBEntiry.Paid = true;
            var updatedTicket = await _ticketRepository.ConfirmPaymentAsync(ticketDBEntiry, CancellationToken.None);

            return new TicketDto()
            {
                Id = updatedTicket.Id,
                CreatedTime = updatedTicket.CreatedTime,
                Paid = updatedTicket.Paid,
                Seats = updatedTicket.Seats.Select(x => x.SeatNumber).ToList(),
                Showtime = updatedTicket.Showtime.SessionDate,
                ShowtimeId = updatedTicket.ShowtimeId
            };
        }
    }
}
