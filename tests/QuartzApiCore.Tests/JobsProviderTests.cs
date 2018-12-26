using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Impl.Triggers;
using QuartzApiCore.API.Services;
using Xunit;

namespace QuartzApiCore.Tests
{
    public class JobsProviderTests
    {
        [Fact]
        public async Task Should_extract_jobs_from_quartz_scheduler()
        {
            var scheduler = A.Fake<IScheduler>();
            var sut = new JobsProvider(scheduler);
            var jobKeys = new ReadOnlyCollection<JobKey>(new List<JobKey>
            {
                new JobKey("Job Key 1"),
                new JobKey("Job Key 2")
            });

            var fakeTrigger = new Fake<ITrigger>();
            fakeTrigger.CallsTo(trigger => trigger.GetNextFireTimeUtc()).Returns(DateTimeOffset.Now.AddHours(1));

            A.CallTo(() => scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup(), default(CancellationToken))).Returns(
                jobKeys
            );
            A.CallTo(() => scheduler.GetTriggersOfJob(A<JobKey>.Ignored, default(CancellationToken))).Returns(
                new ReadOnlyCollection<ITrigger>(new List<ITrigger> {fakeTrigger.FakedObject}));


            var quartzJobs = await sut.GetScheduledJobs();

            A.CallTo(() => scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup(), default(CancellationToken)))
                .MustHaveHappened();
            A.CallTo(() => scheduler.GetTriggersOfJob(A<JobKey>.Ignored, default(CancellationToken)))
                .MustHaveHappened();
            A.CallTo(() => scheduler.GetJobDetail(A<JobKey>.Ignored, default(CancellationToken)))
                .MustHaveHappenedOnceOrMore();

            var quartzJob = quartzJobs.First();
            
            quartzJob.Triggers.Should().NotBeEmpty();
            quartzJob.Triggers.First().NextFireTime.Should().BeCloseTo(DateTimeOffset.Now.AddHours(1), 5000);
        }

        [Fact]
        public void Should_execute_a_job()
        {
            var scheduler = A.Fake<IScheduler>();
            var sut = new JobsProvider(scheduler);
            var jobGroup = "DEFAULT";
            var jobName = "Test 1";

            sut.ExecuteJob(jobGroup, jobName);

            A.CallTo(() =>
                    scheduler.TriggerJob(A<JobKey>.That.Matches(key => key.Group == jobGroup && key.Name == jobName),
                        default(CancellationToken)))
                .MustHaveHappened();
        }

        [Fact]
        public void Should_pause_a_job()
        {
            var scheduler = A.Fake<IScheduler>();
            var sut = new JobsProvider(scheduler);
            var jobGroup = "DEFAULT";
            var jobName = "Test 1";

            sut.PauseJob(jobGroup, jobName);

            A.CallTo(() =>
                    scheduler.PauseJob(A<JobKey>.That.Matches(key => key.Group == jobGroup && key.Name == jobName),
                        default(CancellationToken)))
                .MustHaveHappened();
        }
    }
}