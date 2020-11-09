using SetCronJob.ApiClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient
{
    public interface ISetCronJobClient
    {
        Task<CronJob> CreateJobAsync(CronJob cronJob);
        Task DeleteJobAsync(int id);
        Task DeleteJobAsync(string name);
        Task<IReadOnlyList<CronJob>> ListJobsAsync(string keyword = null);
        Task<CronJob> UpdateJobAsync(CronJob cronJob);
    }
}