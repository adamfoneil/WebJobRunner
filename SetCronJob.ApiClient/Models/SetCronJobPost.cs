using Newtonsoft.Json;

namespace SetCronJob.ApiClient.Models
{
    /// <summary>
    /// SetCronJob requires a token property with all posts, so I inline it into all objects 
    /// </summary>
    public class SetCronJobPost
    {
        [JsonProperty("token")]
        public string Token { get;  set; }
    }
}
