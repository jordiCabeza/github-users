using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

using GitHubUsers.Dtos;
using GitHubUsers.Models;
using GitHubUsers.Service;

namespace GitHubUsers.Managers
{
    public class GitHubManager : IGitHubManager
    {
        public const string GitHubApiUserUrl = @"https://api.github.com/users/";
        private readonly IGitHubService gitHubService;        

        public GitHubManager(IGitHubService gitHubService)
        {
            this.gitHubService = gitHubService;
        }

        public async Task<User> GetGitHubUserByUsername(string username)
        {
            var userUrl = string.Format("{0}{1}", GitHubApiUserUrl, username);
            var gitHubUser = await gitHubService.Get<GitHubUser>(userUrl);

            if (gitHubUser == null)
            {
                return null;
            }

            var gitHubRepos = await gitHubService.Get<IList<GitHubRepo>>(gitHubUser.ReposUrl);

            var topFiveRepos = gitHubRepos.OrderByDescending(repo => repo.Stars).Take(5);
            return new User
                       {
                           Name = gitHubUser.Name,
                           Username = gitHubUser.Login,
                           AvatarUrl = gitHubUser.AvatarUrl,
                           Location = gitHubUser.Location,
                           Repositories = topFiveRepos.Select(
                                   repo => new Repository { Name = repo.Name, Stars = repo.Stars, Url = repo.Url })
                               .ToList()                               
                       };
        }
    }
}