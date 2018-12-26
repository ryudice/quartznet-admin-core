using System;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using QuartzApiCore.API.Domain;

namespace SampleConsoleApp
{
    public static class SchedulerSetup
    {

        public static IScheduler CreateScheduler()
        {
            var job1 = CreateDummyJob("Hello World 1");
            var job2 = CreateDummyJob("Hello World 2");

            var trigger1 = CreateTrigger("HelloTrigger 1", 30);
            var trigger2 = CreateTrigger("HelloTrigger 2", 60);

            var stdSchedulerFactory = new StdSchedulerFactory();

            var scheduler = stdSchedulerFactory.GetScheduler().Result;

            scheduler.ScheduleJob(job1, trigger1);
            scheduler.ScheduleJob(job2, trigger2);
            
            return scheduler;
        }


        private static IJobDetail CreateDummyJob(string jobName)
        {
            var helloWorldJob = JobBuilder.Create<HelloWorldJob>()
                .WithIdentity(jobName)
                .Build();

            return helloWorldJob;
        }

        private static ITrigger CreateTrigger(string name, int interval)
        {
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(name)
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(interval)
                    .RepeatForever())
                .Build();

            return trigger;
        }
    }
    
    [QuartzApiJobParameter("testParameter")]
    public class HelloWorldJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello World");
            return Task.CompletedTask;
        }
    }
}