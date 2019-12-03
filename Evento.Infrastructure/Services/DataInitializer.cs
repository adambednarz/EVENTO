using Evento.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evento.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        private readonly ILogger<DataInitializer> _logger;


        public DataInitializer(IUserService userService, IEventService eventService,
            ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _eventService = eventService;
            _logger = logger;
        }


        public async Task SeedAsync()
        {
            _logger.LogInformation("Data initializing.....");
            var tasks = new List<Task>();

            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "User1", "user1@email.com", "secret", "user"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "User2", "user2@email.com", "secret", "user"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "Admin", "admin@admin.com", "secret", "admin"));

            _logger.LogInformation("Created users");

            for (int i = 0; i < 10; i++)
            {
                Guid id = Guid.NewGuid();
                var name = "Event " + $"{i}";
                var description = $"{name} - description";
                DateTime startDate = DateTime.UtcNow.AddDays(1);
                DateTime endDate = startDate.AddDays(1);
                tasks.Add(_eventService.CreateAsync(id, name, description, startDate, endDate));
                tasks.Add(_eventService.AddTicketsAsync(id, 1000, 100));
                _logger.LogInformation($"Created {name} events");
            }
            await Task.WhenAll(tasks);
            _logger.LogInformation("Data was initialized");
        }
    }
}
