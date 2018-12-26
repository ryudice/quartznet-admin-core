using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using QuartzApiCore.API;
using QuartzApiCore.API.Services;
using QuartzApiCore.Tests.Fixtures;

namespace QuartzApiCore.Tests
{
    public class CustomWebApplicationFactory :
        WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder.ConfigureServices(services =>
            {
                var quartzSchedulerFixture = new QuartzSchedulerFixture();
                services.AddSingleton(quartzSchedulerFixture.Scheduler);
                services.AddTransient<IJobsProvider, JobsProvider>();
            });
        }
    }
}