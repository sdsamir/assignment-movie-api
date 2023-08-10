using Challange.Movies.Domain.Abstructions;
using Challange.Movies.Domain.Entities;
using Challange.Movies.Domain.Sql.Context;
using Microsoft.EntityFrameworkCore;

namespace Challange.Movies.Domain.Sql.Repositories
{
    public class MovieRepository: IMovieRepository
    {
        private readonly CinemaContext _context;

        public MovieRepository(CinemaContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Movie> GetAsync(int id)
        {
            return await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Movie>> GetAsync()
        {
            return await _context.Movies.ToListAsync();
        }
    }
}
