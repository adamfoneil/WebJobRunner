using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Refit;
using SetCronJob.ApiClient.Interfaces;
using SetCronJob.ApiClient.Models;
using System;
using System.Threading.Tasks;

namespace SetCronJob.ApiClient
{
    public class Client
    {
        private readonly string _token;
        private readonly ISetCronJobApi _api;

        private JObject _apiData;

        public Client(string token)
        {
            _token = token;
            _api = RestService.For<ISetCronJobApi>("https://www.setcronjob.com", new RefitSettings()
            {
                ExceptionFactory = async (response) =>
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<ApiResponse>(json);
                    if (result.IsSuccess)
                    {
                        _apiData = result.Data as JObject;
                        return null;
                    }

                    return new Exception(result.ToString());
                }
            });
        }

        public async Task<CronJob> CreateJobAsync(CronJob cronJob)
        {
            await _api.CreateJobAsync(SetToken(cronJob));
            return _apiData.ToObject<CronJob>();
        }
            
        public async Task DeleteJob(int id) => await _api.DeleteJobAsync(_token, id);

        private T SetToken<T>(T @object) where T : SetCronJobPost
        {
            @object.Token = _token;
            return @object;
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
