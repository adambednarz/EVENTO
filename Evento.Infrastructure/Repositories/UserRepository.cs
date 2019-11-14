using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Repositories
{
    public class UserRepository : IUserReposiotory
    {
        private readonly static ISet<User> _users = new HashSet<User>(); 
        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));


        public async Task<IEnumerable<User>> BrowseAsync(string name = "")
        {
            var users = _users.AsEnumerable();
            if(!string.IsNullOrWhiteSpace(name))
            {
                users = users.Where(x => x.Name.Contains(name));
            }

            return await Task.FromResult(users);
        }

        public async Task DeleteAsync(User user)
        {
            _users.Remove(user);
            await Task.CompletedTask;
        }

        public async Task UpdatedAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}
