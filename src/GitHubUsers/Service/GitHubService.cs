using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using GitHubUsers.Dtos;

using Newtonsoft.Json.Linq;

namespace GitHubUsers.Service
{
    public class GitHubService : IGitHubService
    {
        private readonly HttpClient httpClient;

        public GitHubService(HttpMessageHandler handler)
        {            
            this.httpClient = new HttpClient(handler);

            httpClient.DefaultRequestHeaders.Add("User-Agent", "GitHubUserApp");
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");            
        }

        public async Task<T> Get<T>(string url) where T : class
        {
            var response = await httpClient.GetAsync(url);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error accessing GitHub API");
            }

            return await response.Content.ReadAsAsync<T>();
        }
    }
}