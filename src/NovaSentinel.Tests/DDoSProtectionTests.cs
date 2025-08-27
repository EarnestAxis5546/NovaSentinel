using Moq;
using NovaSentinel.Filters;
using Xunit;

namespace NovaSentinel.Tests
{
    public class DDoSProtectionTests
    {
        [Fact]
        public async Task Middleware_BlocksRateLimitedIp()
        {
            var redisMock = new Mock<RedisClient>();
            redisMock.Setup(r => r.IsRateLimited(It.IsAny<string>())).ReturnsAsync(true);
            var middleware = new DDoSProtectionMiddleware(context => Task.CompletedTask, redisMock.Object);
            var context = new Mock<HttpContext>();
            context.Setup(c => c.Connection.RemoteIpAddress).Returns(System.Net.IPAddress.Parse("127.0.0.1"));

            await middleware.InvokeAsync(context.Object);

            context.Verify(c => c.Response.StatusCode = 429, Times.Once());
        }
    }
}