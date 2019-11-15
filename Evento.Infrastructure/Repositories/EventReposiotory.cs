using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private static readonly ISet<Event> _events = new HashSet<Event>
        {
            new Event(Guid.NewGuid(), "Event1", "Event1 - description", DateTime.UtcNow.AddHours(11), DateTime.UtcNow.AddHours(20)),
            new Event(Guid.NewGuid(), "Event2", "Event2 - description", DateTime.UtcNow.AddHours(11), DateTime.UtcNow.AddHours(20)),
            new Event(Guid.NewGuid(), "Event3", "Event3 - description", DateTime.UtcNow.AddHours(11), DateTime.UtcNow.AddHours(20)),
            new Event(Guid.NewGuid(), "Event4", "Event4 - description", DateTime.UtcNow.AddHours(11), DateTime.UtcNow.AddHours(20)),
            new Event(Guid.NewGuid(), "Event5", "Event5 - description", DateTime.UtcNow.AddDays(11), DateTime.UtcNow.AddDays(20))
        };

        public async Task AddAsync(Event @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        public async Task<Event> GetAsync(Guid id)
            => await Task.FromResult(_events.SingleOrDefault(x => x.Id == id));

        public async Task<Event> GetAsync(string name)
            => await Task.FromResult(_events.SingleOrDefault(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant()));


        public async Task<IEnumerable<Event>> BrowseAsync(string name = "")
        {
            var events = _events.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                events = events.Where(x => x.Name.Contains(name));
            }
            return await Task.FromResult(events);
        }

        public async Task DeleteAsync(Event @event)
        {
            _events.Remove(@event);
            await Task.CompletedTask;
        }

        public async Task UpdatedAsync(Event @event)
        {
            await Task.CompletedTask;
        }
    }
}
