using Challange.Movies.Domain.Entities;

namespace Challange.Movies.Domain.Abstructions
{
    public interface IAuditoriumRepository
    {
        Task<IEnumerable<Auditorium>> GetAllAsync(CancellationToken cancel);
        Task<Auditorium> GetAsync(int auditoriumId, CancellationToken cancel);
    }
}
