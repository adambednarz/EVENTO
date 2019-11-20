using Evento.Core.Domain;
using Evento.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Extensions
{
    public static class RepositoriesExtensions
    {
        public static async Task<User> GetOrFail(this IUserRepository userRepository, string email)
        {
            var user = await userRepository.GetAsync(email);

            if (user == null)
                throw new Exception($"User with email: '{email}' does not exist.");

            return user;
        }

        public static async Task<Event> GetOrFail(this IEventRepository eventRepository, Guid userId)
        {
            var @event = await eventRepository.GetAsync(userId);

            if(@event == null)
                throw new Exception($"User with id: '{userId}' does not exist.");

            return @event;
        }
    }
}
