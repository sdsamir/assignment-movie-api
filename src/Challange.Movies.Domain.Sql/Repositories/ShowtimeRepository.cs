using Challange.Movies.Domain.Abstructions;
using Challange.Movies.Domain.Entities;
using Challange.Movies.Domain.Sql.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Challange.Movies.Domain.Sql.Repositories
{
    public class ShowtimeRepository : IShowtimeRepository
    {
        private readonly CinemaContext _context;

        public ShowtimeRepository(CinemaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Showtime> CreateAsync(Showtime showTime, CancellationToken cancel)
        {
            var showtimeDBEntity = await _context.Showtimes.AddAsync(showTime, cancel);
            var response  = await _context.SaveChangesAsync(cancel);
            return showtimeDBEntity.Entity;
        }

        public async Task<Showtime> GetAsync(int id, CancellationToken cancel)
        {
            return await _context.Showtimes
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Showtime>> GetAsync(Expression<Func<Showtime, bool>> filter, CancellationToken cancel)
        {
            if (filter == null)
            {
                return await _context.Showtimes
                .ToListAsync();
            }
            return await _context.Showtimes
                .Where(filter)
                .ToListAsync();
        }

    }
}
