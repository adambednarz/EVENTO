using Evento.Core.Domain;
using Evento.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services.Interfaces
{
     public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task<IEnumerable<UserDto>> BrowseAsync();
        Task RegisterAsync(Guid userId, string name,
            string email, string password, string role = "user");
        Task<TokenDto> LoginAsync(string email, string password);

    }
}
