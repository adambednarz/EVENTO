using Evento.Core.Domain;
using Evento.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services.Interfaces
{
    public interface IEventService
    {
        Task CreateAsync(Guid id, string name, string description,
            DateTime startDate, DateTime endDate);
        Task<EventDto> GetAsync(Guid id);
        Task<EventDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowsAsync(string name = null);
        Task AddTicketsAsync(Guid eventId, int amount, decimal price);
        Task UpdateAsync(Guid id, string name, string description);
        Task RemoveAsync(Guid id);
    }
}
