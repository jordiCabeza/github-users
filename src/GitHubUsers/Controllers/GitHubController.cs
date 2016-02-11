using System.Threading.Tasks;
using System.Web.Http;

using GitHubUsers.Managers;

namespace GitHubUsers.Controllers
{
    public class GitHubController : ApiController
    {
        private readonly IGitHubManager gitHubManager;

        public GitHubController(IGitHubManager gitHubManager)
        {
            this.gitHubManager = gitHubManager;
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string id)
        {
            var user = await gitHubManager.GetGitHubUserByUsername(id);
            if (user == null)
            {
                return NotFound();
            }

            return this.Ok(user);
        }        
    }
}
