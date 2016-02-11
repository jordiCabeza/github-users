using Newtonsoft.Json;

namespace GitHubUsers.Dtos
{
    public class GitHubUser
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("location")]
        public string Location { get; set; }
        
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        
        [JsonProperty("repos_url")]
        public string ReposUrl { get; set; }
        
    }    
}
