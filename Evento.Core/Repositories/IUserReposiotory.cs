using Evento.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Core.Repositories
{
    public interface IUserReposiotory
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> BrowseAsync(string name = "");
        Task AddAsync(User user);
        Task UpdatedAsync(User user);
        Task DeleteAsync(User user);
    }
}
