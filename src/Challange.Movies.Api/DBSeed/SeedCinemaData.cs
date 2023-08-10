using Challange.Movies.Domain.Entities;
using Challange.Movies.Domain.Sql.Context;

namespace Challange.Movies.Api.DBSeed
{
    public class SeedCinemaData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<CinemaContext>();
            context.Database.EnsureCreated();

            context.Auditoriums.Add(new Auditorium
            {
                Id = 1,
                Showtimes = new List<Showtime>
                {
                    new Showtime
                    {
                        Id = 1,
                        SessionDate = new DateTime(2023, 08, 10),
                        Movie = new Movie
                        {
                            Id = 1,
                            Title = "Inception",
                            ImdbId = "tt1375666",
                            ReleaseDate = new DateTime(2010, 01, 14),
                            Stars = "Leonardo DiCaprio, Joseph Gordon-Levitt, Ellen Page, Ken Watanabe"
                        },
                        AuditoriumId = 1
                    }
                },
                Seats = GenerateSeats(1, 10, 10)
            });;

            context.Auditoriums.Add(new Auditorium
            {
                Id = 2,
                Seats = GenerateSeats(2, 10, 10)
            });

            context.Auditoriums.Add(new Auditorium
            {
                Id = 3,
                Seats = GenerateSeats(3, 10, 10)
            });

            context.SaveChanges();
        }

        private static List<Seat> GenerateSeats(int auditoriumId, short rows, short cols)
        {
            int id = 0;
            var seats = new List<Seat>();
            for (short r = 1; r <= rows; r++)
                for (short c = 1; c <= cols; c++)
                {
                    var seatNumber = GenerateSeatNumber(r, c);
                    seats.Add(new Seat { Id = ++id, AuditoriumId = auditoriumId, Row = r, Col = c, SeatNumber = seatNumber });
                }
            return seats;
        }

        private static string GenerateSeatNumber(short row, short col)
        {
            var seatRow = string.Empty;
            while (row > 0)
            {
                var modulo = (row - 1) % 26;
                seatRow = Convert.ToChar('A' + modulo) + seatRow;
                row = (short)((row - modulo) / 26);
            }
            return $"{seatRow}{col}";
        }
    }
}
