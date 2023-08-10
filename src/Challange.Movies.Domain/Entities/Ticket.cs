namespace Challange.Movies.Domain.Entities
{
    public class Ticket
    {
        public Ticket()
        {
            CreatedTime = DateTime.Now;
            Paid = false;
        }

        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Paid { get; set; }

        public int ShowtimeId { get; set; }
        public virtual Showtime Showtime { get; set; }

        public virtual ICollection<Seat> Seats { get; set; }

    }
}
