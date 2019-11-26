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
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IEventService eventService,
            IUserService userService, ILogger<DataInitializer> logger)
        {
            _eventService = eventService;
            _userService = userService;
            _logger = logger;
        }
        public async Task Seed()
        {
            _logger.LogInformation("Data initializing....");
            var tasks = new List<Task>();
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "User1", "user1@email.com", "password", "user"));
            tasks.Add(_userService.RegisterAsync(Guid.NewGuid(), "Admin", "admin@admin.com", "secret", "admin"));
            _logger.LogInformation($"Created 2 accounts: User and Admin");

            for (int i = 1; i =< 10; i++)
            {
                var id = Guid.NewGuid();
                var name = "Event " + $"{i}";
                var description = name + " - description";
                DateTime startDate = DateTime.UtcNow.AddDays(1);
                DateTime endDate = startDate.AddDays(1);
                tasks.Add(_eventService.CreateAsync(id, name, description, startDate, endDate));
                tasks.Add(_eventService.AddTicketsAsync(id, 1000, 100));

                _logger.LogInformation($"Created '{name}' event");
            }

            await Task.WhenAll(tasks);


        }
    }
}
