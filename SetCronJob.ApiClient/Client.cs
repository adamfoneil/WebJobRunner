using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using SetCronJob.ApiClient.Exceptions;
using SetCronJob.ApiClient.Interfaces;
using SetCronJob.ApiClient.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient
{
    public class Client : ISetCronJobClient
    {
        private readonly string _token;
        private readonly ISetCronJobApi _api;

        public Client(string token)
        {
            _token = token;
            _api = RestService.For<ISetCronJobApi>("https://www.setcronjob.com");
        }

        public async Task<CronJob> CreateJobAsync(CronJob cronJob)
        {
            cronJob.Token = _token;
            var result = await _api.CreateJobAsync(cronJob);
            return result.GetResult();
        }

        public async Task DeleteJobAsync(int id) => await _api.DeleteJobAsync(_token, id);

        public async Task DeleteJobAsync(string name)
        {
            var searchJobs = await ListJobsAsync(name);
            if (searchJobs.Count == 1)
            {
                await DeleteJobAsync(searchJobs.First().Id);
                return;
            }

            throw new Exception($"Can't delete more than one job at a time: {string.Join(", ", searchJobs.Take(3).Select(j => j.Id))}");
        }

        public async Task<IReadOnlyList<CronJob>> ListJobsAsync(string keyword = null)
        {
            var result = await _api.ListJobsAsync(_token, keyword);
            return result.GetResult();
        }

        public async Task<CronJob> UpdateJobAsync(CronJob cronJob)
        {
            cronJob.Token = _token;
            var result = await _api.UpdateJobAsync(cronJob);
            return result.GetResult();
        }
    }
}
