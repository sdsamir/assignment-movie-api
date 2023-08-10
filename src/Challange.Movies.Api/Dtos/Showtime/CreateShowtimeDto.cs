using System.ComponentModel.DataAnnotations;

namespace Challange.Movies.Api.Dtos.Showtime
{
    public class CreateShowtimeDto
    {
        [Required]
        public DateTime SessionDate { get; set; }

        [Required]
        public int AuditoriumId { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}
