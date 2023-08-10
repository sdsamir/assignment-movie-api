using Challange.Movies.Api.Dtos.Showtime;
using Challange.Movies.Api.Services.Showtime;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    public class ShowtimeController : Controller
    {
        private readonly IShowtimeService _showtimeService;

        public ShowtimeController(IShowtimeService showtimeService)
        {
            _showtimeService = showtimeService ?? throw new ArgumentNullException(nameof(showtimeService));
        }

        [HttpPost("showtimes")]
        public async Task<ActionResult<ShowtimeDto>> PostAsync([FromBody] CreateShowtimeDto showtime)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _showtimeService.CreateAsync(showtime));
        }

        [HttpGet("showtimes")]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> GetAsync()
        {
            var showTimes = await _showtimeService.GetAllAsync();
            return Ok(showTimes);
        }

        [HttpGet("{id:int}/showtimes")]
        public async Task<ActionResult<IEnumerable<ShowtimeDto>>> GetAsync(int id)
        {
            var showTimes = await _showtimeService.GetAsync(id);
            return Ok(showTimes);
        }
    }
}
