using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
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

        [Fact]
        public async Task get_user_async_should_invoke_get_async_on_repository()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new User(id, "user", "Adam", "email@gmail.com", "secret");
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var userMapperMock = new Mock<IMapper>();
            userMapperMock.Setup(x => x.Map<UserDto>(user)).Returns(userDto);
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var userService = new UserService(userRepositoryMock.Object, userMapperMock.Object, jwtHandlerMock.Object);


            //Act
            await userService.GetUserAsync(user.Id);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once);
            
        }
    }
}
