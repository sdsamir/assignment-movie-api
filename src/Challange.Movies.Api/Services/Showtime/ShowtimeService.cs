using Challange.Movies.Domain.Abstructions;
using System.Linq.Expressions;
using Challange.Movies.Api.Dtos;
using AutoMapper;
using Challange.Movies.Domain.Entities;
using System.Collections.Generic;
using Challange.Movies.Api.Dtos.Showtime;
using Challange.Movies.Api.Dtos.Ticket;
using Challange.Movies.Api.Dtos.Movie;

namespace Challange.Movies.Api.Services.Showtime
{
    public class ShowtimeService : IShowtimeService
    {
        public IAuditoriumRepository _auditoriumRepository;
        private readonly IShowtimeRepository _showtimeRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly ITicketRepository _ticketRepository;

        private readonly IMapper _mapper;
        public ShowtimeService(
            IAuditoriumRepository auditoriumRepository,
            IShowtimeRepository showtimeRepository,
            IMovieRepository movieRepository,
            ITicketRepository ticketRepository,
            IMapper mapper)
        {
            _auditoriumRepository = auditoriumRepository ?? throw new ArgumentNullException(nameof(auditoriumRepository));
            _showtimeRepository = showtimeRepository ?? throw new ArgumentNullException(nameof(showtimeRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ShowtimeDto>> GetAllAsync()
        {
            var showTimeList = await _showtimeRepository.GetAsync(null, CancellationToken.None);
            var showTimes = _mapper.Map<IEnumerable<ShowtimeDto>>(showTimeList).ToList();

            foreach (var showTime in showTimeList)
            {
                var st = showTimes.FirstOrDefault(s => s.Id == showTime.Id);
                st.Movie = new MovieDto() 
                            { 
                                Id = showTime.Movie.Id, 
                                ImdbId = showTime.Movie.ImdbId, 
                                Stars = showTime.Movie.Stars, 
                                ReleaseDate = showTime.Movie.ReleaseDate, 
                                Title = showTime.Movie.Title 
                            };
                st.Tickets = showTime
                            .Tickets
                            .Select(x => 
                                new TicketDto() 
                                    {
                                        Id = x.Id, 
                                        Paid = x.Paid, 
                                        Seats = x.Seats.Select(s => s.SeatNumber).ToList() 
                                    }
                                )
                            .ToList();
            }
            return showTimes;
        }

        public async Task<ShowtimeDto> GetAsync(int id)
        {
            var showTimeDBEntity = await _showtimeRepository.GetAsync(id, CancellationToken.None);

            var showTime = new ShowtimeDto();
            showTime.Id = showTimeDBEntity.Id;
            showTime.AuditoriumId = showTimeDBEntity.AuditoriumId;
            showTime.Movie = new MovieDto()
                                { 
                                    Id = showTimeDBEntity.Movie.Id, 
                                    ImdbId = showTimeDBEntity.Movie.ImdbId, 
                                    Stars = showTimeDBEntity.Movie.Stars, 
                                    ReleaseDate = showTimeDBEntity.Movie.ReleaseDate, 
                                    Title = showTimeDBEntity.Movie.Title 
                                };
            showTime.Tickets = showTimeDBEntity
                .Tickets
                .Select(x => 
                        new TicketDto() 
                            { 
                                Id = x.Id, 
                                Paid = x.Paid, 
                                Seats = x.Seats.Select(s => s.SeatNumber).ToList() 
                            }
                    )
                .ToList();

            return showTime;
        }

        public async Task<ShowtimeDto> CreateAsync(CreateShowtimeDto showtime)
        {
            var st = new Domain.Entities.Showtime()
            {
                SessionDate = showtime.SessionDate,
                Tickets = null
            };
            var movie = await _movieRepository.GetAsync(showtime.MovieId);
            st.Movie = movie ?? throw new NullReferenceException($"Movie not found. Movie Id :{showtime.MovieId}");

            var auditoriumDBEntity = await _auditoriumRepository.GetAsync(showtime.AuditoriumId, CancellationToken.None);
            st.AuditoriumId = auditoriumDBEntity?.Id ?? throw new NullReferenceException($"Auditorium not found. Auditorium Id :{showtime.AuditoriumId}");


            var showtimeDbEntity = await _showtimeRepository.CreateAsync(st, CancellationToken.None);
            var createdShowtime = new ShowtimeDto()
            {
                Id = showtimeDbEntity.Id,
                AuditoriumId = showtimeDbEntity.AuditoriumId,
                Movie = new MovieDto() { Id = showtimeDbEntity.Movie.Id, Title = showtimeDbEntity.Movie.Title },
                Tickets = null
            };
            return createdShowtime;
        }

        public async Task<TicketDto> BookTicket(CreateTicketDto createTicket)
        {
            Domain.Entities.Ticket ticket = new Domain.Entities.Ticket();

            var showtime = await _showtimeRepository.GetAsync(createTicket.ShowtimeId, CancellationToken.None);
            var totalSeats = showtime.Auditorium.Seats.OrderBy(x => x.Id).ToList();

            var bookedSeats = showtime
            .Tickets
            //.Where(t => t.Paid == true || t.CreatedTime > DateTime.Now.AddMinutes(-3))
            .Select(x => x.Seats)
            .ToList();

            var autoCancelledSeats = showtime
            .Tickets.Where(t => t.Paid == false && t.CreatedTime < DateTime.Now.AddMinutes(-3))
            .Select(x => x.Seats)
            .ToList();

            var assignedSeats = new List<Seat>();
            if(autoCancelledSeats.Any())
            {
                if(createTicket.SeatCount % 2 ==0)
                {
                    
                }
                else
                {

                }
            }
            else{
                assignedSeats = totalSeats
                                .Skip(bookedSeats.Sum(x => x.Count))
                                .Take(createTicket.SeatCount).ToList();
            }

            

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

        public async Task<TicketDto> BuyTicket(CreateTicketDto createTicket)
        {
            throw new NotImplementedException();
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
                    Seats = x.Seats.Select(x => x.SeatNumber).ToList(),
                    Showtime = x.Showtime.SessionDate,
                    ShowtimeId = x.ShowtimeId
                }
            );

            return tickets;
        }
    }
}