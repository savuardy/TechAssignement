using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Poq.DataSourceClient.Clients;
using Poq.DataSourceClient.Configurations;
using Poq.DataSourceClient.Exceptions;
using Xunit;

namespace Poq.UnitTests.Clients
{
    public class ProductClientTests
    {
        private AutoMocker? _autoMocker;
       
        [Fact]
        public void GetProductsAsync_Invalid_ThrowsEndpointIncorrectException()
        {
            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(new MockyConfiguration()));
            var client = _autoMocker.CreateInstance<ProductClient>();

            Assert.ThrowsAsync<EndpointIncorrectException>(async () => await client.GetProductsAsync(default));
        }
    }
}
