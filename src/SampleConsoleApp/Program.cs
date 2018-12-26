using System;

namespace SampleConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var scheduler = SchedulerSetup.CreateScheduler();
            scheduler.Start();
            QuartzApiCore.API.QuartzApiCore.Start(scheduler);
            
            Console.WriteLine("Hello World!");
        }
    }
}