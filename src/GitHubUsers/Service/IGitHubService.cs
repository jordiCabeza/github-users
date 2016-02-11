using System.Threading.Tasks;

namespace GitHubUsers.Service
{
    public interface IGitHubService
    {
        Task<T> Get<T>(string url) where T : class;
    }
}