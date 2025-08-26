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
            if (await _redis.IsRateLimited(ip))
            {
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }
            await _next(context);
        }
    }
}