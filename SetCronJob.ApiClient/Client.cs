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
    public class Client
    {
        private readonly string _token;
        private readonly ISetCronJobApi _api;

        private JObject _apiObjectData;
        private JArray _apiArrayData;

        public Client(string token)
        {
            _token = token;
            _api = RestService.For<ISetCronJobApi>("https://www.setcronjob.com", new RefitSettings()
            {
                ExceptionFactory = async (response) =>
                {
                    if (response.StatusCode == HttpStatusCode.ServiceUnavailable)
                    {
                        return new ServiceUnavailableException();
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(json);
                    if (result.IsSuccess)
                    {
                        _apiObjectData = result.Data as JObject;
                        _apiArrayData = result.Data as JArray;
                        return null;
                    }

                    _apiObjectData = null;
                    _apiArrayData = null;
                    return new Exception(result.ToString());
                }
            });
        }

        public async Task<CronJob> CreateJobAsync(CronJob cronJob) => await ExecuteWithTokenAsync(cronJob, async () => await _api.CreateJobAsync(cronJob));

        public async Task DeleteJobAsync(int id) => await _api.DeleteJobAsync(_token, id);

        public async Task DeleteJobAsync(string name)
        {
            var searchJobs = await ListJobsAsync(name);
            if (searchJobs.Count == 1)
            {
                await DeleteJobAsync(searchJobs.First().Id);
                return;
            }

            throw new Exception($"Can't delete more than one job at a time: {string.Join(", ", searchJobs.Take(3).Select(j => j.Name))}");
        }

        public async Task<IReadOnlyList<CronJob>> ListJobsAsync(string keyword = null) => await ExecuteAsync<List<CronJob>>(async () => await _api.ListJobsAsync(_token, keyword));

        public async Task<CronJob> UpdateJobAsync(CronJob cronJob) => await ExecuteWithTokenAsync(cronJob, async() => await _api.UpdateJobAsync(_token, cronJob));

        private async Task<T> ExecuteWithTokenAsync<T>(T @object, Func<Task> apiCall) where T : SetCronJobPost
        {
            @object.Token = _token;
            await apiCall.Invoke();
            return GetApiResult<T>();
        }

        private async Task<T> ExecuteAsync<T>(Func<Task> apiCall) where T : class
        {
            await apiCall.Invoke();
            return GetApiResult<T>();
        }

        private T GetApiResult<T>() where T : class
        {
            if (_apiObjectData != null) return _apiObjectData.ToObject<T>();
            if (_apiArrayData != null) return _apiArrayData.ToObject<T>();
            throw new Exception("No API result data was returned.");
        }

        private class ApiResponse
        {
            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("data")]
            public object Data { get; set; }

            public bool IsSuccess => (Status.Equals("success"));

            public override string ToString()
            {
                return
                    (Status.Equals("success")) ? "Success" :
                    (Status.Equals("error")) ? $"{Message} (code {Code})" :
                    string.Empty;
            }
        }
    }
}
