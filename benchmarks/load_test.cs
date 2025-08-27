using BenchmarkDotNet.Attributes;
using NovaSentinel.Services;
using Moq;

namespace NovaSentinel.Benchmarks
{
    [MemoryDiagnoser]
    public class LoadTest
    {
        private readonly DDoSService _service;
        private readonly Mock<RedisClient> _redisMock;

        public LoadTest()
        {
            _redisMock = new Mock<RedisClient>();
            _redisMock.Setup(r => r.IsRateLimitedAsync(It.IsAny<string>())).ReturnsAsync(false);
            _service = new DDoSService(_redisMock.Object);
        }

        [Benchmark]
        public async Task RateLimitCheck()
        {
            await _service.IsRateLimitedAsync("127.0.0.1");
        }

        [Benchmark]
        public void BlockIp()
        {
            _service.BlockIp("127.0.0.1");
        }
    }
}