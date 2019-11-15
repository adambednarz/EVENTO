using AutoMapper;
using Evento.Core.Domain;
using Evento.Core.Repositories;
using Evento.Infrastructure.Dto;
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
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
                return null;

            return _mapper.Map<UserDto>(user);
        }

        public async Task LoginAsync(string email, string password)
        {
            await Task.CompletedTask;
        }

        public async Task RegisterAsync(Guid userId, string name, 
            string email, string password, string role)
        {
            var user = await _userRepository.GetAsync(email);
            if (user != null)
                throw new Exception($"User with email: '{email}' already exist.");

            user = new User(userId, role, name, email, password);
            await _userRepository.AddAsync(user);
        }
    }
}
