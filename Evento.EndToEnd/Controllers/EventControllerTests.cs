using Evento.Api;
using Evento.Infrastructure.Dto;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Evento.Tests.EndToEnd.Controllers
{
    public class EventControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        public EventControllerTests()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }
        [Fact]
        public async Task featching_events_should_retund_not_empty_collection()
        {
            var response = await _client.GetAsync("events");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<IEnumerable<EventDto>>(content);

            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);
            events.Should().NotBeEmpty();
        }
    }
}