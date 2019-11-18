using AutoMapper;
using Evento.Core.Domain;
using Evento.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evento.Infrastructure.Mapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Event, EventDto>()
                    .ForMember(x => x.TicketAmount, m => m.MapFrom(p => p.Tickets.Count()));
                cfg.CreateMap<Event, EventDetailsDto>();
                cfg.CreateMap<Ticket, TicketDto>();
                cfg.CreateMap<User, UserDto>();
            })
            .CreateMapper();
    }
}
