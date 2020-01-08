using Evento.Infrastructure.Commands;
using Evento.Infrastructure.Commands.Users;
using Evento.Infrastructure.Services.Interfaces;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Handlers.User
{
    public class LoginUserHandler : ICommandHandler<LoginUser>
    {
        private readonly IUserService _userService;

        public LoginUserHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task HandleAsync(LoginUser command)
        {
            var log = await _userService.LoginAsync(command.Email, command.Password);
        }
    }
}
