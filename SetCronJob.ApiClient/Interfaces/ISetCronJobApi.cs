using Refit;
using SetCronJob.ApiClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient.Interfaces
{
    internal interface ISetCronJobApi
    {
        [Post("/api/cron.add")]
        Task CreateJobAsync([Body]CronJob cronJob);

        [Post("/api/cron.delete?token={token}&id={id}")]
        Task DeleteJobAsync(string token, int id);

        [Get("/api/cron.list?token={token}&keyword={keyword}")]
        Task ListJobsAsync(string token, string keyword);
    }
}
