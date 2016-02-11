using Newtonsoft.Json;

namespace GitHubUsers.Dtos
{
    public class GitHubRepo
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("html_url")]
        public string Url { get; set; }

        [JsonProperty("stargazers_count")]
        public int Stars { get; set; }       
    }
}
