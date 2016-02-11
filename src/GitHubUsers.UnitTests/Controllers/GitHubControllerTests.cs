using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Results;

using GitHubUsers.Controllers;
using GitHubUsers.Managers;
using GitHubUsers.Models;

using Moq;

using NUnit.Framework;

namespace GitHubUsers.UnitTests.Controllers
{
    public class GitHubControllerTests
    {
        private GitHubController gitHubController;

        private Mock<IGitHubManager> mockGitHubManager;

        [SetUp]
        public void SetUp()
        {
            mockGitHubManager = new Mock<IGitHubManager>();
            gitHubController = new GitHubController(mockGitHubManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            mockGitHubManager.VerifyAll();
        }

        [Test]
        public async void ShouldGetUserByUsername()
        {
            // Arrange
            var username = "username";
            var user = new User
            {
                Name = "Name",
                Username = username
            };                       

            mockGitHubManager.Setup(manager => manager.GetGitHubUserByUsername(username)).ReturnsAsync(user);

            // Act
            var result = await gitHubController.Get(username) as OkNegotiatedContentResult<User>;

            // Assert
            Assert.NotNull(result);
            Assert.That(result.Content, Is.SameAs(user));
        }

        [Test]
        public async void ShouldGetUserByUsernameReturnNotFoundIfUserIsNull()
        {
            // Arrange
            var username = "username";            

            mockGitHubManager.Setup(manager => manager.GetGitHubUserByUsername(username)).ReturnsAsync(null);

            // Act
            var result = await gitHubController.Get(username) as NotFoundResult;

            // Assert
            Assert.NotNull(result);            
        } 
    }
}