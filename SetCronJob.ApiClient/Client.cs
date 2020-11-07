using Refit;
using SetCronJob.ApiClient.Interfaces;
using SetCronJob.ApiClient.Models;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient
{
    public class Client
    {
        private readonly string _token;
        private readonly ISetCronJobApi _api;

        public Client(string token)
        {
            _token = token;
            _api = RestService.For<ISetCronJobApi>("https://www.setcronjob.com");
        }

        public async Task<CronJob> CreateJobAsync(CronJob cronJob) => await _api.CreateJobAsync(_token, cronJob);

        public async Task DeleteJob(int id) => await _api.DeleteJobAsync(_token, id);
    }
}
