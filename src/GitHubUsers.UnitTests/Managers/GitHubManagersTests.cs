using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using GitHubUsers.Dtos;
using GitHubUsers.Managers;
using GitHubUsers.Models;
using GitHubUsers.Service;

using Moq;

using NUnit.Framework;

namespace GitHubUsers.UnitTests.Managers
{
    [TestFixture]
    public class GitHubManagersTests
    {
        private Mock<IGitHubService> mockGitHubService;

        private GitHubManager gitHubManager;
        
        [SetUp]
        public void SetUp()
        {
            mockGitHubService = new Mock<IGitHubService>();
            gitHubManager = new GitHubManager(mockGitHubService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            mockGitHubService.VerifyAll();
        }

        [Test]
        public async void ShouldGetGitHubUserByUsername()
        {
            // Arrange
            var username = "username";
            var gitHubUser = new GitHubUser
                                 {
                                     Name = "name",
                                     Login = username,
                                     Location = "somePlace",
                                     AvatarUrl = "someUrl",
                                     ReposUrl = "someReposUrl"
                                 };

            var gitRepos = new List<GitHubRepo>
                               {
                                   new GitHubRepo { Name = "repo1", Stars = 1, Url = "someUrl" },
                                   new GitHubRepo { Name = "repo7", Stars = 7, Url = "someUrl" },                                   
                                   new GitHubRepo { Name = "repo4", Stars = 4, Url = "someUrl" },
                                   new GitHubRepo { Name = "repo6", Stars = 6, Url = "someUrl" },
                                   new GitHubRepo { Name = "repo3", Stars = 3, Url = "someUrl" },
                                   new GitHubRepo { Name = "repo5", Stars = 5, Url = "someUrl" },
                                   new GitHubRepo { Name = "repo2", Stars = 2, Url = "someUrl" }
                               };                               

            mockGitHubService
                .Setup(service => service.Get<GitHubUser>(string.Format("{0}{1}", GitHubManager.GitHubApiUserUrl, username)))
                .ReturnsAsync(gitHubUser);

            mockGitHubService
                .Setup(service => service.Get<IList<GitHubRepo>>(gitHubUser.ReposUrl))
                .ReturnsAsync(gitRepos);

            // Act
            var user = await gitHubManager.GetGitHubUserByUsername(username);
            
            // Assert
            Assert.IsNotNull(user);
            Assert.That(user.Name, Is.EqualTo(gitHubUser.Name));
            Assert.That(user.Username, Is.EqualTo(gitHubUser.Login));
            Assert.That(user.Location, Is.EqualTo(gitHubUser.Location));
            Assert.That(user.AvatarUrl, Is.EqualTo(gitHubUser.AvatarUrl));

            Assert.IsNotNull(user.Repositories);
            Assert.That(user.Repositories.Count, Is.EqualTo(5));
            Assert.That(user.Repositories[0].Name, Is.EqualTo("repo7"));
            Assert.That(user.Repositories[0].Stars, Is.EqualTo(7));
            Assert.That(user.Repositories[1].Name, Is.EqualTo("repo6"));
            Assert.That(user.Repositories[1].Stars, Is.EqualTo(6));
            Assert.That(user.Repositories[2].Name, Is.EqualTo("repo5"));
            Assert.That(user.Repositories[2].Stars, Is.EqualTo(5));
            Assert.That(user.Repositories[3].Name, Is.EqualTo("repo4"));
            Assert.That(user.Repositories[3].Stars, Is.EqualTo(4));
            Assert.That(user.Repositories[4].Name, Is.EqualTo("repo3"));
            Assert.That(user.Repositories[4].Stars, Is.EqualTo(3));
        }

         [Test]
        public async void ShouldGetGitHubUserByUsernameReturnNullIfUserDoesNotExists()
        {
            // Arrange
            var username = "invalidUsername";
            
            mockGitHubService
                .Setup(service => service.Get<GitHubUser>(string.Format("{0}{1}", GitHubManager.GitHubApiUserUrl, username)))
                .ReturnsAsync(null);
            
            // Act
            var user = await gitHubManager.GetGitHubUserByUsername(username);
            
            // Assert
            Assert.IsNull(user);            
        }
    }
}