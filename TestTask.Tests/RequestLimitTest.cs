using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace TestTask.Tests.RequestLimitTest
{
    public class RequestLimitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public RequestLimitTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Should_Return_429_After_10_Requests()
        {
            var client = _factory.CreateClient();
            var tasks = Enumerable.Range(1, 11)
                .Select(_ => client.GetAsync("/dogs"))
                .ToArray();

            await Task.WhenAll(tasks);

            var responses = tasks.Select(t => t.Result).ToArray();
            int okCount = responses.Count(r => r.StatusCode == HttpStatusCode.OK);
            int tooManyCount = responses.Count(r => r.StatusCode == HttpStatusCode.TooManyRequests);

            Assert.Equal(10, okCount);
            Assert.Equal(1, tooManyCount);
        }
    }
}