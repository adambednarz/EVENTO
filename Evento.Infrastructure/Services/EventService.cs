using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private IEventRepository _eventRepository;

        public  EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }


        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            await Task.CompletedTask;
        }

        public async Task<EventDto> GetAsync(Guid id)
        {
            var @event = await _eventRepository.GetAsync(id);

            if (@event == null)
                return null;

            return new EventDto
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                TicketAmount = @event.Tickets.Count()
            };
        }

        public async Task<EventDto> GetAsync(string name)
        {
            var @event = await _eventRepository.GetAsync(name);

            if (@event == null)
                return null;

            return new EventDto
            {
                Id = @event.Id,
                Name = @event.Name,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                TicketAmount = @event.Tickets.Count()
            };
        }

        public async Task<IEnumerable<EventDto>> BrowsAsync(string name = null)
        {
            var events = await _eventRepository.BrowseAsync(name);

            if (events == null)
                return null;
            return events.Select(eve => new EventDto
            {
                Id = eve.Id,
                Name = eve.Name,
                Description = eve.Description,
                StartDate = eve.StartDate,
                EndDate = eve.EndDate,
                TicketAmount = eve.Tickets.Count()
            });
        }

        public async Task AddTicketsAsync(Guid eventId, int amount, decimal price)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            await Task.CompletedTask;
        }
    }
}
