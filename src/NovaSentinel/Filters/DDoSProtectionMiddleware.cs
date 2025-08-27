using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NovaSentinel.Filters
{
    public class DDoSProtectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RedisClient _redis;

        public DDoSProtectionMiddleware(RequestDelegate next, RedisClient redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ip))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Invalid IP address.");
                return;
            }

            if (await _redis.IsRateLimitedAsync(ip))
            {
                context.Response.StatusCode = 429;
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }

            await _next(context);
        }
    }
}