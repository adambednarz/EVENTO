using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Command.Events;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string name)
        {
            var events = await _eventService.BrowsAsync(name);

            return Json(events);
        }

        [HttpGet("{eventid}")]
        public async Task<IActionResult> Get(Guid eventId)
        {

            var @event = await _eventService.GetAsync(eventId);

            if(@event == null)
            {
                return NotFound();
            }
            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEvent command)
        {
            command.Id = Guid.NewGuid();
            await _eventService.CreateAsync(command.Id, command.Name,
                command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketsAsync(command.Id, command.TicketsAmount, command.TiketPrice);

            return Created($"/events/{command.Id}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody] UpdateEvent command)
        {
            var @event = _eventService.GetAsync(eventId);
            if (@event == null)
                return NotFound();

            await _eventService.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            var @event = await _eventService.GetAsync(eventId);
            if (@event == null)
                return NotFound();

            await _eventService.RemoveAsync(eventId);

            return NoContent();
        }
    }
}