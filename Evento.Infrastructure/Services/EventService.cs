using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
             
        public  EventService(IEventRepository eventRepository, 
            IMapper mapper, ILogger<EventService> logger
            )
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }


        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _eventRepository.GetAsync(name);

            if (@event != null)
            {
                throw new Exception($"Event named: '{name}' already exist.");
            };

            @event = new Event(id, name, description, startDate, endDate);
            await _eventRepository.AddAsync(@event);
        }

        public async Task<EventDetailsDto> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);

            if (@event == null)
                return null;

            return _mapper.Map<EventDetailsDto>(@event);
        }

        public async Task<EventDetailsDto> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);

            if (@event == null)
                return null;


            return _mapper.Map<EventDetailsDto>(@event); 
            
            //return new EventDto
            //{
            //    Id = @event.Id,
            //    Name = @event.Name,
            //    Description = @event.Description,
            //    StartDate = @event.StartDate,
            //    EndDate = @event.EndDate,
            //    TicketAmount = @event.Tickets.Count()
            //};
        }

        public async Task<IEnumerable<EventDto>> BrowsAsync(string name = null)
        {
            _logger.LogTrace("Zalogowano użycie metody BrowsAsync");
            var events = await _eventRepository.BrowseAsync(name);

            if (events == null)
                return null;

            return _mapper.Map<IEnumerable<EventDto>>(events);
            
            //return events.Select(eve => new EventDto
            //{
            //    Id = eve.Id,
            //    Name = eve.Name,
            //    Description = eve.Description,
            //    StartDate = eve.StartDate,
            //    EndDate = eve.EndDate,
            //    TicketAmount = eve.Tickets.Count()
            //});
        }

        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            var @event = await _eventRepository.GetAsync(eventId);

            if(@event == null)
            {
                throw new Exception($"Event with id: '{eventId}' does not exist");
            }
            @event.AddTickets(amount, price);
            await _eventRepository.UpdatedAsync(@event);
        }

        public async Task RemoveAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);
            await _eventRepository.DeleteAsync(@event);
            
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var @event = await _eventRepository.GetAsync(name);
            if(@event != null)
            {
                throw new Exception($"Event with name: '{name}' already exist");
            }
            @event = await _eventRepository.GetAsync(id);
            @event.SetName(name);
            @event.SetDescription(description);
            await _eventRepository.UpdatedAsync(@event);
        }
    }
}
