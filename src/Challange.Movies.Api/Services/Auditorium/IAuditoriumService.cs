using Challange.Movies.Api.Dtos.Auditorium;

namespace Challange.Movies.Api.Services.Auditorium
{
    public interface IAuditoriumService
    {
        Task<IEnumerable<AuditoriumDto>> GetAllAsync();
        Task<AuditoriumDto> GetAsync(int auditoriumId);
    }
}
