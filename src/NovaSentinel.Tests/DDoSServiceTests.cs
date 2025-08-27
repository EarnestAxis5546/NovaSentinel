using Moq;
using NovaSentinel.Services;
using Xunit;

namespace NovaSentinel.Tests
{
    public class DDoSServiceTests
    {
        [Fact]
        public void BlockIp_CallsRedis()
        {
            var redisMock = new Mock<RedisClient>();
            var service = new DDoSService(redisMock.Object);

            service.BlockIp("127.0.0.1");

            redisMock.Verify(r => r.BlockIp("127.0.0.1"), Times.Once());
        }
    }
}