using Challange.Movies.Api.Dtos.Movie;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    public class MovieController : Controller
    {
        [HttpGet("movies")]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetAsync()
        {
            return Ok();
        }
    }
}
