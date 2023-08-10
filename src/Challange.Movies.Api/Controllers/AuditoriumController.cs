using Challange.Movies.Api.Dtos.Auditorium;
using Challange.Movies.Api.Services.Auditorium;
using Microsoft.AspNetCore.Mvc;

namespace Challange.Movies.Api.Controllers
{
    [ApiController]
    public class AuditoriumController : Controller
    {
        private readonly IAuditoriumService _auditoriumService;
        public AuditoriumController(IAuditoriumService auditoriumService)
        {
            _auditoriumService = auditoriumService ?? throw new ArgumentNullException(nameof(auditoriumService));
        }

        [HttpGet("auditoriums")]
        public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetAsync()
        {
            return Ok(await _auditoriumService.GetAllAsync());
        }

        [HttpGet("{id:int}/auditoriums")]
        public async Task<ActionResult<IEnumerable<AuditoriumDto>>> GetAsync(int id)
        {
            return Ok(await _auditoriumService.GetAsync(id));
        }
    }
}
