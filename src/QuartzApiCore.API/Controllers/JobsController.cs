using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuartzApiCore.API.Domain;
using QuartzApiCore.API.Services;

namespace QuartzApiCore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobsProvider _jobsProvider;

        public JobsController(IJobsProvider jobsProvider)
        {
            _jobsProvider = jobsProvider;
        }

        [HttpGet]
        public async Task<IEnumerable<QuartzJob>> Get()
        {
            var jobs = await _jobsProvider.GetScheduledJobs();
            return jobs;
        }

        [HttpPost]
        [Route("{jobGroup}/{jobName}/trigger")]
        public async Task<IActionResult> Trigger(string jobGroup, string jobName)
        {
            _jobsProvider.ExecuteJob(jobGroup, jobName);
            return Ok();
        }
        
        [HttpPost]
        [Route("{jobGroup}/{jobName}/pause")]
        public async Task<IActionResult> Pause(string jobGroup, string jobName)
        {
            _jobsProvider.PauseJob(jobGroup, jobName);
            return Ok();
        }
    }
}