using System;
using System.Threading;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace QuartzApiCore.Tests.Fixtures
{
    public class QuartzSchedulerFixture
    {
        public QuartzSchedulerFixture()
        {
            var stdSchedulerFactory = new StdSchedulerFactory();

            Scheduler = stdSchedulerFactory.GetScheduler().Result;

            var helloWorldJob = JobBuilder.Create<HelloWorldJob>()
                .WithIdentity("HelloWorld")
                .Build();
            
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(40)
                    .RepeatForever())
                .Build();

            Scheduler.ScheduleJob(helloWorldJob, trigger);
        }

        public IScheduler Scheduler { get; }
    }

    public class HelloWorldJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello World");
            return Task.CompletedTask;
        }
    }
}