using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Handlers.User
{
    public class RegisterUserHandler : ICommandHandler<RegisterUser>
    {
        private readonly IUserService _userService;

        public RegisterUserHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(RegisterUser command)
        {
            await _userService.RegisterAsync(command.Id, command.Name,
            command.Email, command.Password, command.Role);
        }
    }
}
