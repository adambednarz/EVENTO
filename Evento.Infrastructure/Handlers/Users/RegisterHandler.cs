using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Handlers.Users
{
    public class RegisterHandler : ICommandHandler<Register>
    {
        private readonly IUserService _userService;

        public RegisterHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(Register command)
        {
            command.Id = Guid.NewGuid();
            await _userService.RegisterAsync(command.Id, command.Name,
                command.Email, command.Password, command.Role);
        }
    }
}
