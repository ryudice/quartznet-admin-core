using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl;
using QuartzApiCore.API.Services;

namespace QuartzApiCore.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
//            services.AddSingleton<IScheduler>(SetupScheduler());
            services.AddTransient<IJobsProvider, JobsProvider>();
        }

        public IScheduler SetupScheduler()
        {
            var stdSchedulerFactory = new StdSchedulerFactory();
            var scheduler =  stdSchedulerFactory.GetScheduler().Result;

          
            
            return scheduler;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
//            app.UseDefaultFiles();
//            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new EmbeddedFileProvider( Assembly.Load(new AssemblyName("QuartzApiCore.API")),"QuartzApiCore.API")
                
            });
            
            app.UseMvc();
        }
    }
}