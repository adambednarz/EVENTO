using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
using Evento.Infrastructure.Extensions;
using Evento.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IJwtHandler _jwtHandler;
        public UserService(IUserRepository userRepository, IMapper mapper,
            IJwtHandler jwthandler)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtHandler = jwthandler;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserAsync(Guid id)
        {
            var user = await _userRepository.GetOrFailAsync(id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync( )
        {
            var users = await _userRepository.BrowseAsync();

            if (users == null)
                return null;

            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<TokenDto> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
                throw new Exception($"Invalid credentials.");

            if (user.Password != password)
                throw new Exception("Invalid credentials.");

            var jwt =  _jwtHandler.CreateToken(user.Id, user.Role);

            return new TokenDto
            {
                Token = jwt.Token,
                Expires = jwt.Expires,
                Role = user.Role
            };

        }

        public async Task RegisterAsync(Guid userId, string name, 
            string email, string password, string role = "User")
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new Exception($"User with email: '{email}' already exist.");

            user = new User(userId, role, name, email, password);
            await _userRepository.AddAsync(user);
        }
    }
}
