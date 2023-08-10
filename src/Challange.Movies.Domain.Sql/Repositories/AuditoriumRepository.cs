using Challange.Movies.Domain.Abstructions;
using Challange.Movies.Domain.Entities;
using Challange.Movies.Domain.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Challange.Movies.Domain.Sql.Repositories
{
    public class AuditoriumRepository : IAuditoriumRepository
    {
        private readonly CinemaContext _context;

        public AuditoriumRepository(CinemaContext context)
        {
            _context = context?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Auditorium>> GetAllAsync(CancellationToken cancel)
        {
            return await _context.Auditoriums.ToListAsync();
        }

        public async Task<Auditorium> GetAsync(int auditoriumId, CancellationToken cancel)
        {
            return await _context.Auditoriums
                .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancel);
        }
    }
}
