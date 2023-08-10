using Challange.Movies.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challange.Movies.Domain.Abstructions
{
    public interface IMovieRepository
    {
        Task<Movie> GetAsync(int id);
        Task<IEnumerable<Movie>> GetAsync();
    }
}
