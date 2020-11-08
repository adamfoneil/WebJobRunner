using System.Text.Json.Serialization;

namespace SetCronJob.ApiClient.Models
{
    /// <summary>
    /// SetCronJob requires a token property with all posts, so I inline it into all objects 
    /// </summary>
    public class SetCronJobPost
    {
        [JsonPropertyName("token")]
        public string Token { get;  set; }
    }
}
