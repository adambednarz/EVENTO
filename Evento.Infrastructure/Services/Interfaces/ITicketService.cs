using Evento.Core.Domain;
using Evento.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDetailsDto>> GetTicketsForUser(Guid userId);
        Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId);
        Task PurchaseAsync(Guid userId, Guid eventId, int amount);
        Task CancelAsync(Guid userId, Guid eventId, int amount);
    }
}
