using Challange.Movies.Api.Dtos.Seat;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    public class SeatController : Controller
    {
        [HttpGet("seats")]
        public async Task<ActionResult<IEnumerable<SeatDto>>> GetAsync()
        {
            return Ok();
        }
    }
}
