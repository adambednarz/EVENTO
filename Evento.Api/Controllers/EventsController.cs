using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Events;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;


namespace Evento.Api.Controllers
{
    public class EventsController : ApiControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;
        private readonly IMemoryCache _cache;

        public EventsController(IEventService eventService,
            ILogger<EventsController> logger, IMemoryCache cache)
        {
            _eventService = eventService;
            _logger = logger;
            _cache = cache;
        }

        [HttpGet]        
        public async Task<IActionResult> Get(string name)
        {
            throw new ArgumentException("Ups...");
            //========== Logger test ===========
            _logger.LogTrace("Trace log");
            _logger.LogDebug("Debug log");
            _logger.LogInformation("Information log");
            _logger.LogError("Error log");
            _logger.LogCritical("Critical log");

            //========== Cache test ===========
            var events = _cache.Get<IEnumerable<EventDto>>("events");
            if(events == null)
            {
                _logger.LogInformation("Fetching from service");
                events = await _eventService.BrowsAsync(name);
                //_cache.Set("events", events, TimeSpan.FromMinutes(2));
                _cache.Set<IEnumerable<EventDto>>("events", events, TimeSpan.FromSeconds(10));
            }
            else
            {
            _logger.LogInformation("Fetching from cache");
            }

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var events = await _eventService.GetAsync(eventId);

            return Json(events);
        }

        [HttpPost]
        [Authorize(Policy ="HasAdminRole")]
        public async Task<IActionResult> Post([FromBody] CreateEvent command)
        {
            command.EventId = Guid.NewGuid();
            await _eventService.CreateAsync(command.EventId, command.Name, 
                command.Description, command.StartDate, command.EndDate);
            await _eventService.AddTicketsAsync(command.EventId, command.Tickets, 
                command.Price);

            return Created($"/events/{command.EventId}", null);
        }

        [HttpPut("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Put(Guid eventId, [FromBody] UpdateEvent command)
        {
            var @event = _eventService.GetAsync(eventId);

            if(@event == null)
            {
                return NotFound();
            }
            await _eventService.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        [Authorize(Policy = "HasAdminRole")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            var @event = _eventService.GetAsync(eventId);

            if (@event == null)
            {
                return NotFound();
            }
            await _eventService.RemoveAsync(eventId);

            return NoContent();
        }
    }
}