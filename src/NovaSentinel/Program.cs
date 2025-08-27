using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;

namespace NovaSentinel
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Optimize thread pool for high concurrency
            ThreadPool.SetMinThreads(100, 100);
            ThreadPool.SetMaxThreads(1000, 1000);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
