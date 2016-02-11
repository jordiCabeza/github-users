using Newtonsoft.Json;

namespace GitHubUsers.UnitTests
{
    public class DummyObject
    {
        [JsonProperty("dummyProperty")]
        public string DummyProperty { get; set; } 
    }
}