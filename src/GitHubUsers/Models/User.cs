using System.Collections;
using System.Collections.Generic;

namespace GitHubUsers.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string AvatarUrl { get; set; }
        public IList<Repository> Repositories { get; set; }
    }
}