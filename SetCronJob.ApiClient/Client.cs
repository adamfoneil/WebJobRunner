using Refit;
using SetCronJob.ApiClient.Interfaces;
using SetCronJob.ApiClient.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            _api = RestService.For<ISetCronJobApi>("https://www.setcronjob.com", new RefitSettings()
            {
                ExceptionFactory = async (response) =>
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var error = JsonSerializer.Deserialize<Error>(json);
                    return new Exception(error.ToString());
                }
            });
        }

        public async Task<CronJob> CreateJobAsync(CronJob cronJob) => await _api.CreateJobAsync(_token, cronJob);

        public async Task DeleteJob(int id) => await _api.DeleteJobAsync(_token, id);

        private class Error
        {
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("code")]
            public int Code { get; set; }

            [JsonPropertyName("message")]
            public string Message { get; set; }

            public override string ToString() => $"{Message} (code {Code})";
        }
    }
}
