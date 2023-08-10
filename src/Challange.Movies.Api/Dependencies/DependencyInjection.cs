using Challange.Movies.Api.Services.Auditorium;
using Challange.Movies.Api.Services.Showtime;
using Challange.Movies.Api.Services.Ticket;
using Challange.Movies.Domain.Abstructions;
using Challange.Movies.Domain.Sql.Context;
using Challange.Movies.Domain.Sql.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Challange.Movies.Api.Dependencies
{
    public static class DependencyInjection
    {
        public static void InjectRepositories(this IServiceCollection services)
        {
            services.AddTransient<IShowtimeRepository, ShowtimeRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<IAuditoriumRepository, AuditoriumRepository>();
            services.AddTransient<IMovieRepository, MovieRepository>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            services.AddTransient<IAuditoriumService, AuditoriumService>();
            services.AddTransient<IShowtimeService, ShowtimeService>();
            services.AddTransient<ITicketService, TicketService>();
        }

        public static void InjectDbContext(this IServiceCollection services)
        {
            services.AddDbContext<CinemaContext>(options =>
            {
                options.UseLazyLoadingProxies().UseInMemoryDatabase("CinemaDb")
                    .EnableSensitiveDataLogging()
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }
    }
}
