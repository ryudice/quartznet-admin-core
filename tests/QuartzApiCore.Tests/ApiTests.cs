using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using QuartzApiCore.API.Domain;
using Xunit;
using Xunit.Abstractions;

namespace QuartzApiCore.Tests
{
    public class ApiTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly ITestOutputHelper _outputHelper;

        public ApiTests(CustomWebApplicationFactory factory, ITestOutputHelper outputHelper)
        {
            _factory = factory;
            _outputHelper = outputHelper;
        }

        [Fact]
        public async Task Should_return_jobs_in_scheduler()
        {
            var httpClient = _factory.CreateClient();

            var httpResponseMessage = await httpClient.GetAsync("/api/jobs");

            var jsonPayload = await httpResponseMessage.Content.ReadAsStringAsync();
            _outputHelper.WriteLine(jsonPayload);

            var jsonJobs = JsonConvert.DeserializeObject<List<QuartzJob>>(jsonPayload);

            jsonJobs.Count.Should().BeGreaterOrEqualTo(1);

            var quartzJob = jsonJobs.First();
            quartzJob.Name.Should().NotBeEmpty();
            quartzJob.Triggers.Count.Should().BeGreaterThan(0);
            
            Assert.Equal(200, (int)httpResponseMessage.StatusCode);
        }

        [Fact]
        public async Task Should_return_triggets_with_jobs()
        {
            var httpClient = _factory.CreateClient();
            
            var httpResponseMessage = await httpClient.GetAsync("/api/triggers");
        }
    }
}