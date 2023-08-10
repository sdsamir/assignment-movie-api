using AutoMapper;
using Challange.Movies.Api.Dtos;
using Challange.Movies.Api.Dtos.Auditorium;
using Challange.Movies.Api.Dtos.Showtime;
using Challange.Movies.Domain.Abstructions;

namespace Challange.Movies.Api.Services.Auditorium
{
    public class AuditoriumService:IAuditoriumService
    {
        public IAuditoriumRepository _auditoriumRepository;
        private readonly IMapper _mapper;
        public AuditoriumService(IAuditoriumRepository auditoriumRepository, IMapper mapper)
        {
            _auditoriumRepository = auditoriumRepository ?? throw new ArgumentNullException(nameof(auditoriumRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<AuditoriumDto>> GetAllAsync()
        {
            var auditoriumDBList = await _auditoriumRepository.GetAllAsync(CancellationToken.None);
            var auditoriums = new List<AuditoriumDto>();

            foreach (var a in auditoriumDBList)
            {
                var auditorium = new AuditoriumDto(); ;

                auditorium.Id = a.Id;
                auditorium.Showtimes = a.Showtimes.Select(y => new ShowtimeDto() { Id = y.Id, SessionDate = y.SessionDate}).ToList();
                auditorium.Seats = a.Seats.Select(y => y.SeatNumber).ToList();
                auditorium.SeatCount = a.Seats.Count();

                auditoriums.Add(auditorium);
            }

            return auditoriums;
        }

        public async Task<AuditoriumDto> GetAsync(int auditoriumId)
        {
            var auditoriumDBEntity = await _auditoriumRepository.GetAsync(auditoriumId, CancellationToken.None);
            
            var auditorium = new AuditoriumDto();

            auditorium.Id = auditoriumDBEntity.Id;
            auditorium.Showtimes = auditoriumDBEntity.Showtimes.Select(y => new ShowtimeDto() { Id = y.Id, SessionDate = y.SessionDate }).ToList();
            auditorium.Seats = auditoriumDBEntity.Seats.Select(y => y.SeatNumber).ToList();
            auditorium.SeatCount = auditoriumDBEntity.Seats.Count();

            return auditorium;
        }
    }
}
