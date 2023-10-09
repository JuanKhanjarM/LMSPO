using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace LMSPO.Tests.CustomerTests
{
    public class CustomersControllerIntegrationTests: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;


        public CustomersControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task GetCustomer_ValidCustomerId_ReturnsOk(int customerId)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"http://localhost:5047/api/customers/{customerId}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
