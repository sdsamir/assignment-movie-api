using Challange.Movies.Api.Dtos.Showtime;
using Challange.Movies.Api.Dtos.Ticket;

namespace Challange.Movies.Api.Services.Showtime
{
    public interface IShowtimeService
    {
        Task<IEnumerable<ShowtimeDto>> GetAllAsync();

        Task<ShowtimeDto> GetAsync(int id);

        Task<ShowtimeDto> CreateAsync(CreateShowtimeDto showtime);
    }
}
