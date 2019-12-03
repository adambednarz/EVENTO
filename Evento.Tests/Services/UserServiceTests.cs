using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Repositories;
using Evento.Infrastructure.Services;
using Evento.Infrastructure.Services.Interfaces;
using FluentAssertions;
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
        public async Task register_async_should_invoke_add_aync_from_user_repository()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, jwtHandlerMock.Object);

            // Act
            await userService.RegisterAsync(Guid.NewGuid(), "Adam", "email@gmail.com", "secret");
            //Assert
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once());
        }

        [Fact]
        public async Task when_invoking_get_asunc_it_should_invoke_get_async_on_user_repository()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "admin", "admin", "email@gmail.com", "pasword");
            var accountDto = new UserDto
            {
                Id = user.Id,
                Role = user.Role,
                Name = user.Name,
                Email = user.Email
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtHandlerMock = new Mock<IJwtHandler>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<UserDto>(user)).Returns(accountDto);

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, jwtHandlerMock.Object);
            userRepositoryMock.Setup(x => x.GetAsync(user.Id))
                .ReturnsAsync(user);

            // Act
            await userService.GetUserAsync(user.Id);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Id), Times.Once());
            accountDto.Should().NotBeNull();
            accountDto.Email.Should().BeEquivalentTo(user.Email);
        }
    }
}
