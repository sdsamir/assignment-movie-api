using Challange.Movies.Domain.Entities;
using System.Linq.Expressions;

namespace Challange.Movies.Domain.Abstructions
{
    public interface IShowtimeRepository
    {
        Task<Showtime> CreateAsync(Showtime showtimeEntity, CancellationToken cancel);
        Task<IEnumerable<Showtime>> GetAsync(Expression<Func<Showtime, bool>> filter, CancellationToken cancel);
        Task<Showtime> GetAsync(int id, CancellationToken cancel);
    }
}
