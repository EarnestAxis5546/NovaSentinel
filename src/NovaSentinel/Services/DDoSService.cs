using System;
using StackExchange.Redis;

namespace NovaSentinel.Services
{
    public class DDoSService
    {
        private readonly RedisClient _redis;

        public DDoSService(RedisClient redis)
        {
            _redis = redis;
        }

        public Models.TrafficStats GetTrafficStats()
        {
            // Placeholder for traffic stats
            return new Models.TrafficStats { BlockedRequests = 0, TotalRequests = 0 };
        }

        public void BlockIp(string ip)
        {
            _redis.BlockIp(ip);
        }

        public async Task<bool> IsRateLimitedAsync(string ip)
        {
            return await _redis.IsRateLimitedAsync(ip);
        }
    }
}