using Newtonsoft.Json;
using System;

namespace SetCronJob.ApiClient.Models
{
    internal class ApiResult<T>
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        public bool IsSuccess => (Status.Equals("success"));

        [JsonProperty("data")]
        public T Data { get; set; }

        public T GetResult() => (IsSuccess) ? Data : throw new Exception(Message);
    }
}
