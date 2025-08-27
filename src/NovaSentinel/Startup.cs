using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NovaSentinel.Filters;
using NovaSentinel.Services;

namespace NovaSentinel
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options => options.AllowEmptyInputInBodyModelBinding = true);
            services.AddSingleton<RedisClient>();
            services.AddSingleton<DDoSService>();
            // Optimize for high concurrency
            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxConcurrentConnections = 35000;
                options.Limits.MaxRequestBodySize = null;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseMiddleware<DDoSProtectionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}