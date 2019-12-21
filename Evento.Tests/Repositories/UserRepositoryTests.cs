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
        public async Task adding_new_new_user_by_add_async_should_create_new_object_in_list()
        public async Task AddAsync_UniqueValidUser_AddedToList()
        {
            //Arange
            var user = new User(Guid.NewGuid(), "admin", "admin", "email@gmail.com", "pasword");
            IUserRepository userRepository = new UserRepository();

            //Act
            await userRepository.AddAsync(user);

            //Assert
            var existingUser = await userRepository.GetAsync("email@gmail.com");
            Assert.Equal(existingUser, user);
        }
    }
}
