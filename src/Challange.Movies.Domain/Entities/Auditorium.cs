namespace Challange.Movies.Domain.Entities
{
    public class Auditorium
    {
        public int Id { get; set; }

        public virtual ICollection<Showtime> Showtimes { get; set; }
        public virtual ICollection<Seat> Seats { get; set; }
    }
}
