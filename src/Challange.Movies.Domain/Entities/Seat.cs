namespace Challange.Movies.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public short Row { get; set; }
        public short Col { get; set; }
        public string SeatNumber { get; set; }

        public int AuditoriumId { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}
