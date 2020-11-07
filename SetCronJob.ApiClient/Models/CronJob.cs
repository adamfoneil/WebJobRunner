using System.Text.Json.Serialization;

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

    public class CronJob
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("expression")]
        public string Expression { get; set; }

        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("method")]
        public string Method { get; set; } = "get";

        [JsonPropertyName("httpHeaders")]
        public string Headers { get; set; }

        [JsonPropertyName("postData")]
        public string PostData { get; set;  }

        [JsonPropertyName("fail")]
        public int FailCount { get; set; }

        [JsonPropertyName("status")]
        public JobStatus Status { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("notify")]
        public NotifyOptions Notify { get; set; }

        [JsonPropertyName("points")]
        public int Points { get; set; }
    }
}
