using Evento.Infrastructure.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evento.Infrastructure.Services.Interfaces
{
    public interface IJwtHandler
    {
         JwtDto CreateToken(Guid userId, string role);

    }
}
