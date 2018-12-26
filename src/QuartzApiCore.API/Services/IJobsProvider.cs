using System.Collections.Generic;
using System.Threading.Tasks;
using Quartz;
using QuartzApiCore.API.Domain;

namespace QuartzApiCore.API.Services
{
    public interface IJobsProvider
    {
        Task<ICollection<QuartzJob>> GetScheduledJobs();
        Task ExecuteJob(string jobGroup, string jobName);
        Task PauseJob(string jobGroup, string jobName);
    }
}