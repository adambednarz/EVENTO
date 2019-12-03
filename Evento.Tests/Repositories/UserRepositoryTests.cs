using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Evento.Tests.Repositories
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task when_adding_new_user_it_should_be_added_correctly_to_the_list()
        {
            //Arange
            var id = Guid.NewGuid();
            var user = new User(id, "user", "Adam", "email@gmail.com", "secret");
            IUserRepository repository = new UserRepository();

            //Act
            await repository.AddAsync(user);

            //Assert
            var existingUser = await repository.GetAsync(user.Id);
            Assert.Equal(user, existingUser);
        }
    }
}
