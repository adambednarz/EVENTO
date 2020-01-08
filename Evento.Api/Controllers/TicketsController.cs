using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [Authorize]
    [Route("events/{eventId}/tickets")]
    public class TicketsController : ApiControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketsController(ICommandDispatcher commandDispatcher, ITicketService ticketService) : base(commandDispatcher) 
        {
            _ticketService = ticketService;
        }


        [HttpGet("{ticketId}")]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = await _ticketService.GetAsync(UserId, eventId, ticketId);

            if (ticket == null)
                return NotFound();

            return Json(ticket);
        }

        [HttpPost("purchase/{amount}")]
        public async Task<IActionResult> Post(Guid eventId, int amount)
        {
            await _ticketService.PurchaseAsync(UserId, eventId, amount);

            return NoContent();
        }

        [HttpDelete("delete/{amount}")]
        public async Task<IActionResult> Delete(Guid eventId, int amount)
        {
            await _ticketService.CancelAsync(UserId, eventId, amount);

            return NoContent();
        }
    }
}