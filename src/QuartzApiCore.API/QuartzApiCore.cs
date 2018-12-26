using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace QuartzApiCore.API
{
    public static class QuartzApiCore
    {
        public static void Start(IScheduler scheduler, int port = 5000)
        {
            
            
            WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(collection => { collection.AddSingleton(scheduler); }
                    )
                .UseUrls($"http://*:{port}")
                .Build()
                .Run();
        }
    }
}