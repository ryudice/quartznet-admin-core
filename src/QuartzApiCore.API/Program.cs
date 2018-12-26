using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace QuartzApiCore.API
{
    [ExcludeFromCodeCoverage ]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
               .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Loopback, 5000); //HTTP port
                })
//               .UseWebRoot("wwwroot/dist")
                .UseStartup<Startup>();
    }
}