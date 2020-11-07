using Refit;
using SetCronJob.ApiClient.Models;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient.Interfaces
{
    internal interface ISetCronJobApi
    {
        [Post("/api/cron.add?token={token}")]
        Task CreateJobAsync(string token, [Body]CronJob cronJob);
    }
}
