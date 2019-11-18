using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var events = await _eventService.GetAsync(eventId);

            return Json(events);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEvent command)
        {
            command.Id = Guid.NewGuid();
            await _eventService.CreateAsync(command.Id, command.Name, 
                command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketsAsync(command.Id, 
                command.TicketsAmount, command.TicketsPrice);

            return Created($"/events/{command.Id}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Post(Guid eventId, [FromBody] UpdateEvent command)
        {
            command.Id = eventId;
            await _eventService.UpdateAsync(command.Id, command.Name, 
                command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
             await _eventService.RemoveAsync(eventId);

            return NoContent();
        }
    }
}