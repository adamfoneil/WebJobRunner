using Newtonsoft.Json;

namespace SetCronJob.ApiClient.Models
{
    /// <summary>
    /// https://www.setcronjob.com/documentation/api/data#status-codes
    /// </summary>
    public enum JobStatus
    { 
        Active = 0,
        Disabled = 1,
        Expired = 2,
        Inactive = 3,
        Failed = 4
    }

    public enum NotifyOptions
    {
        Never = 0,
        Failure = 1,
        Always = 2,
        Disabled = 3
    }

    public class CronJob : SetCronJobPost
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("expression")]
        public string Expression { get; set; }

        [JsonProperty("timezone")]
        public string TimeZone { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; } = "get";

        [JsonProperty("httpHeaders")]
        public string Headers { get; set; }

        [JsonProperty("postData")]
        public string PostData { get; set;  }

        [JsonProperty("fail")]
        public int FailCount { get; set; }

        [JsonProperty("status")]
        public JobStatus Status { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("notify")]
        public NotifyOptions Notify { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }
    }
}
