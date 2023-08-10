using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challange.Movies.Domain.Entities
{
    public class Showtime
    {
        public int Id { get; set; }
        public DateTime SessionDate { get; set; }

        public int AuditoriumId { get; set; }
        public virtual Auditorium Auditorium { get; set; }

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
