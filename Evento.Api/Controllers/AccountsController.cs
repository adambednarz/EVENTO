using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Evento.Api.Controllers
{
    public class AccountsController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly ICommandDispatcher _commandDispatcher;

        public AccountsController(IUserService userService,
            ITicketService ticketService, ICommandDispatcher commandDispatcher)
        {
            _userService = userService;
            _ticketService = ticketService;
            _commandDispatcher = commandDispatcher;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _userService.GetUserAsync(UserId);

            return Json(user);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTicketsForUser()
        {
            var user = await _ticketService.GetTicketsForUser(UserId);

            return Json(user);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetUserAsync(email);

            return Json(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] Register command)
        {
            //command.Id = Guid.NewGuid();
            //await _userService.RegisterAsync(command.Id, command.Name, 
            //    command.Email, command.Password, command.Role);
            await _commandDispatcher.DispatchAsync(command);
            return Created($"/accounts/{command.Email}", null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] Login command)
        {
            var log = await _userService.LoginAsync(command.Email, command.Password);

            return Json(log);
        }
    }
}