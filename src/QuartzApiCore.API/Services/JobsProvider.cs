using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl.Matchers;
using QuartzApiCore.API.Domain;

namespace QuartzApiCore.API.Services
{
    public class JobsProvider : IJobsProvider
    {
        private readonly IScheduler _scheduler;

        public JobsProvider(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public async Task<ICollection<QuartzJob>> GetScheduledJobs()
        {
            var jobKeys = await _scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());

            return jobKeys.Aggregate( new List<QuartzJob>(), (jobs, jobKey) =>
            {
                var jobDetail = _scheduler.GetJobDetail(jobKey).Result;

                var quartzJob = new QuartzJob();

                quartzJob.Name = jobDetail.Key.Name;
                quartzJob.Group = jobDetail.Key.Group;
                quartzJob.Parameters = GetParameters(jobDetail).ToArray();
                
                var triggers = _scheduler.GetTriggersOfJob(jobDetail.Key).Result;

                quartzJob.Triggers = triggers.Select(trigger => new QuartzTrigger()
                {
                    Name = trigger.Key.Name,
                    Group = trigger.Key.Group,
                    NextFireTime = trigger.GetNextFireTimeUtc()

                }).ToList();

                jobs.Add(quartzJob);
                
                return jobs;
            });
            
            

        
        }

        public async Task ExecuteJob(string jobGroup, string jobName)
        {
            await _scheduler.TriggerJob(new JobKey(jobName, jobGroup));
        }

        public async Task PauseJob(string jobGroup, string jobName)
        {
            await _scheduler.PauseJob(new JobKey(jobName, jobGroup));
        }

        private ICollection<string> GetParameters(IJobDetail jobDetail)
        {
            var attributes = jobDetail.JobType.GetCustomAttributes(typeof(QuartzApiJobParameterAttribute), true).Cast<QuartzApiJobParameterAttribute>();
            return attributes.Select(attribute => attribute.Name).ToArray();
        }
    }

    public class QuartzTrigger
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTimeOffset? NextFireTime { get; set; }
    }
}