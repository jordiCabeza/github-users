using System.Threading.Tasks;

using GitHubUsers.Models;

namespace GitHubUsers.Managers
{
    public interface IGitHubManager
    {
        Task<User> GetGitHubUserByUsername(string username);
    }
}