using System;
using System.Net;
using System.Net.Http;
using System.Text;

using GitHubUsers.Service;

using NUnit.Framework;

namespace GitHubUsers.UnitTests.Service
{
    [TestFixture]
    public class GitHubServiceTests
    {
        private GitHubService service;

        private FakeMessageHandler handler;

        [SetUp]
        public void SetUp()
        {
            handler = new FakeMessageHandler();
            service = new GitHubService(handler);
        }

        [Test]
        public async void ShouldGetSpecifiedObjectForAGivenUrlWhenServiceSucceed()
        {
            // Arrange
            var url = "http://domain.test/api/fake/5";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
            responseMessage.Content = new StringContent("{ 'dummyProperty' : 'someValue' }", Encoding.Default, "application/json");

            handler.AddFakeResponse(new Uri(url), responseMessage);

            // Act
            
            var response = await service.Get<DummyObject>(url);

            // Assert
            Assert.IsNotNull(response);
            Assert.That(response.DummyProperty, Is.EqualTo("someValue"));
        }

        [Test]
        public async void ShouldGetReturnNullIfServiceReturnsNotFound()
        {
            // Arrange
            var url = "http://domain.test/api/fake/5";
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);            

            handler.AddFakeResponse(new Uri(url), responseMessage);

            // Act

            var response = await service.Get<DummyObject>(url);

            // Assert
            Assert.IsNull(response);
        }

        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.BadRequest)]
        public async void ShouldGetThrowsExceptionIfServiceReturnsOtherStatusThanOkOrNotFound(HttpStatusCode statusCode)
        {
            // Arrange
            var url = "http://domain.test/api/fake/5";
            var responseMessage = new HttpResponseMessage(statusCode);

            handler.AddFakeResponse(new Uri(url), responseMessage);

            Exception exception = null;

            try
            {
                await service.Get<DummyObject>(url);

            }
            catch (Exception e)
            {
                exception = e;
            }
            // Assert
            Assert.IsNotNull(exception);
        }       
    }
}