using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}