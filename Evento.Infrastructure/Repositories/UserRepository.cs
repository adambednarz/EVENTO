using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly static ISet<User> _users = new HashSet<User>
        {
            new User(Guid.NewGuid(), "admin","Adam Bednarz", "bednarz@gmail.com", "secret"),
            new User(Guid.NewGuid(), "user", "User 2", "user2@gmail.com", "secret"),
            new User(Guid.NewGuid(), "user", "User 3", "user3@gmail.com", "secret"),
            new User(Guid.NewGuid(), "user", "User 4", "user4@gmail.com", "secret")
        };
        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }
        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));


        public async Task<IEnumerable<User>> BrowseAsync()
        {
            var users = _users.AsEnumerable();

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
