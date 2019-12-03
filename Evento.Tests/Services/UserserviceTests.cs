using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Services;
using Evento.Infrastructure.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Evento.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task register_async_should_invoke_add_async_on_user_repository()
        {
            //Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var userMapperMock = new Mock<IMapper>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var userService = new UserService(userRepositoryMock.Object, userMapperMock.Object, jwtHandlerMock.Object);

            //Act
            await userService.RegisterAsync(Guid.NewGuid(), "Adam", "email@emaila.com", "secret");

            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
