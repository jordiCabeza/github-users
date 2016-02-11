using System.ComponentModel.Design;
using System.Web.Mvc;

using GitHubUsers.Controllers;

using NUnit.Framework;

namespace GitHubUsers.UnitTests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController homeController;

        [SetUp]
        public void SetUp()
        {
            homeController = new HomeController();
        }

        [Test]
        public void ShouldIndexReturnIndexView()
        {
            // Arrange
            
            // Act
            var result = homeController.Index() as ViewResult;

            // Assert
            Assert.That(result.ViewBag.Title, Is.EqualTo("GitHubUsers"));
            Assert.That(result.ViewName, Is.EqualTo("Index"));
        }
    }
}