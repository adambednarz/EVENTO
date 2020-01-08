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
        public async Task RegisterAsync_ValidParameters_InvokeAddAsyncOnRepository()
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
        public async Task GetAsync_ValidUserId_Invoke_GetAsyncOnRepository()
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

        [Fact]
        public async Task GetAsync_ValidUserEmail_Invoke_GetAsyncOnRepository()
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
            userRepositoryMock.Setup(x => x.GetAsync(user.Email))
                .ReturnsAsync(user);

            // Act
            await userService.GetUserAsync(user.Email);

            //Assert
            userRepositoryMock.Verify(x => x.GetAsync(user.Email), Times.Once());
            accountDto.Should().NotBeNull();
            accountDto.Email.Should().BeEquivalentTo(user.Email);
        }

        [Fact]
        public async Task LoginAsync_ValidUserCredentials_InvokeCreateToken()
        {
            // Arrange
            var user = new User(Guid.NewGuid(), "admin", "admin", "email@gmail.com", "pasword");
            var jwtDto = new JwtDto
            {
                Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9" +
                ".eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ" +
                ".SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
                Expires = 1516239022,
             };

        var mapperMock = new Mock<IMapper>();
        var userRepositoryMock = new Mock<IUserRepository>();
        var jwtHandlerMock = new Mock<IJwtHandler>();
            jwtHandlerMock.Setup(x => x.CreateToken(user.Id, user.Role)).Returns(jwtDto);

        var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, jwtHandlerMock.Object);
        userRepositoryMock.Setup(x => x.GetAsync(user.Email)).ReturnsAsync(user);

        // Act
        await userService.LoginAsync(user.Email, user.Password);

        //Assert
        userRepositoryMock.Verify(x => x.GetAsync(user.Email), Times.Once());
        jwtHandlerMock.Verify(x => x.CreateToken(user.Id, user.Role), Times.Once());
        }
    }
}
