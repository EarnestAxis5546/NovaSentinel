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
            services.AddControllers();
            services.AddSingleton<RedisClient>();
            services.AddSingleton<DDoSService>();
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