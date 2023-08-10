using AutoMapper;
using Challange.Movies.Api.Dtos.Auditorium;
using Challange.Movies.Api.Dtos.Movie;
using Challange.Movies.Api.Dtos.Seat;
using Challange.Movies.Api.Dtos.Showtime;
using Challange.Movies.Api.Dtos.Ticket;
using Challange.Movies.Domain.Entities;

namespace Challange.Movies.Api.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Auditorium, AuditoriumDto>().ReverseMap();
            CreateMap<Movie, MovieDto>().ReverseMap();
            CreateMap<Seat, SeatDto>().ReverseMap();
            CreateMap<Showtime, ShowtimeDto>().ReverseMap();
            CreateMap<Ticket, TicketDto>().ReverseMap();
        }
    }
}
