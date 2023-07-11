using Microsoft.Extensions.Options;
using Moq.AutoMock;
using Poq.Application.Configurations;
using Poq.Application.Interfaces;
using Poq.Application.Models;
using Poq.Application.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Poq.UnitTests.Services
{
    public class ProductServiceTests
    {
        private AutoMocker? _autoMocker;

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        public async Task GetProductsResponse_ValidMinPrice_ReturnsCorrectProducts(float minPrice)
        {
            //Arrange
            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product"),
                new Product("product2", 11, new List<string>{ "large" }, "test product"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product")
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();
            clientMock
                .Setup(x => x.GetProductsAsync(default))
                .Returns(Task.FromResult(products as IEnumerable<Product>));

            var productRequest = new ProductRequestDTO()
            {
                MinPrice = minPrice
            };

            var service = _autoMocker.CreateInstance<ProductService>();
            
            //Act
            var result = await service.GetProductsResponse(productRequest, new CancellationToken());
            
            //Assert
            Assert.True(result.Products.All(x => x.Price >= minPrice));
        }

        [Theory]
        [InlineData(10)]
        [InlineData(12)]
        [InlineData(15)]
        public async Task GetProductsResponse_ValidMaxPrice_ReturnsCorrectProducts(float maxPrice)
        {
            //Arrange
            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product"),
                new Product("product2", 11, new List<string>{ "large" }, "test product"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product")
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();
            clientMock.Setup(x => x.GetProductsAsync(default))
                      .Returns(Task.FromResult(products as IEnumerable<Product>));

            var productRequest = new ProductRequestDTO()
            {
                MaxPrice = maxPrice
            };

            var service = _autoMocker.CreateInstance<ProductService>();
            
            //Act
            var result = await service.GetProductsResponse(productRequest, new CancellationToken());

            //Assert
            Assert.True(result.Products.All(x => x.Price <= maxPrice));
        }

        [Theory]
        [InlineData("small")]
        [InlineData("medium")]
        [InlineData("large")]
        public async Task GetProductsResponse_ValidSize_ReturnsCorrectProducts(string size)
        {
            //Arrange
            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product"),
                new Product("product2", 11, new List<string>{ "large" }, "test product"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product")
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();
            clientMock.Setup(x => x.GetProductsAsync(default))
                      .Returns(Task.FromResult(products as IEnumerable<Product>));

            var productRequest = new ProductRequestDTO()
            {
                Size = size
            };

            var service = _autoMocker.CreateInstance<ProductService>();

            //Act
            var result = await service.GetProductsResponse(productRequest, new CancellationToken());

            //Assert
            Assert.True(result.Products.All(x => x.Sizes.Contains(size)));
        }

        [Fact]
        public async Task GetProductsResponse_ValidHighlighter_ReturnsCorrectProducts()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product"),
                new Product("product2", 11, new List<string>{ "large" }, "test product"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product"),
                new Product("product4", 15, new List<string>{ "medium" }, "fake data")
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();

            clientMock.Setup(x => x.GetProductsAsync(default))
                      .Returns(Task.FromResult(products as IEnumerable<Product>));

            var productRequest = new ProductRequestDTO()
            {
                Highlight = "test,product"
            };

            var service = _autoMocker.CreateInstance<ProductService>();
            
            //Act
            var result = await service.GetProductsResponse(productRequest, new CancellationToken());

            Assert.True(result.Products.All(x => !string.IsNullOrWhiteSpace(x.Description)));
            //Assert
            Assert.True(result.Products.Where(x => x.Description.Contains("<em>test</em>") 
                                                && x.Description.Contains("<em>product</em>")).Count() == 4);
        }

        [Fact]
        public async Task GetProductsResponse_ValidRequest_ReturnsCorrectMinMaxPriceInFilter()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product"),
                new Product("product2", 11, new List<string>{ "large" }, "test product"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product"),
                new Product("product4", 15, new List<string>{ "medium" }, "fake data")
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();

            clientMock.Setup(x => x.GetProductsAsync(default))
                      .Returns(Task.FromResult(products as IEnumerable<Product>));
            var service = _autoMocker.CreateInstance<ProductService>();
            var productRequest = new ProductRequestDTO { MaxPrice = 25 };
            //Act
            var result = await service.GetProductsResponse(productRequest, new CancellationToken());

            Assert.True(result.Filter.MaxPrice == 15 && result.Filter.MinPrice == 5);
        }

        [Fact]
        public async Task GetProductsResponse_ValidRequest_ReturnsCorrectCommonWordsInFilter()
        {
            //Arrange
            var expected = new List<string> { "trouser", "red", "green", "boots", "shirt", "black", "socks", "white", "jacket", "towel" };

            var products = new List<Product>
            {
                new Product("product0", 10, new List<string>{ "small","large" }, "test product for common words trouser red"),
                new Product("product1", 5, new List<string>{ "medium","large" }, "test product for common words trouser green"),
                new Product("product2", 11, new List<string>{ "large" }, "test product for common words boots red"),
                new Product("product3", 15, new List<string>{ "medium" }, "test product for common words shirt black"),
                new Product("product4", 15, new List<string>{ "medium" }, "test product for common words socks white"),
                new Product("product5", 10, new List<string>{ "small","large" }, "test product for common words shirt green"),
                new Product("product6", 5, new List<string>{ "medium","large" }, "test product for common words jacket"),
                new Product("product7", 11, new List<string>{ "large" }, "test product for common words towel yellow"),
                new Product("product8", 15, new List<string>{ "medium" }, "test product for common words shirt black"),
                new Product("product9", 10, new List<string>{ "small","large" }, "test product for common words socks white"),
                new Product("product10", 5, new List<string>{ "medium","large" }, "test product for common words jacket brown"),
                new Product("product11", 11, new List<string>{ "large" }, "test product for common words boots green"),
                new Product("product12", 15, new List<string>{ "medium" }, "test product for common words trouser white"),
                new Product("product13", 10, new List<string>{ "small","large" }, "test product for common words boots black"),
                new Product("product14", 5, new List<string>{ "medium","large" }, "test product for common words shirt red"),
                new Product("product15", 11, new List<string>{ "large" }, "test product for common words jacket black"),
                new Product("product16", 15, new List<string>{ "medium" }, "test product for common words shirt green"),
            };

            var logicConfiguration = new FilterLogicConfiguration
            {
                SkipWords = 5,
                TakeWords = 10,
                Highlighter = "em"
            };

            _autoMocker = new AutoMocker();
            _autoMocker.Use(Options.Create(logicConfiguration));
            var clientMock = _autoMocker.GetMock<IProductClient>();
            clientMock.Setup(x => x.GetProductsAsync(default))
                      .Returns(Task.FromResult(products as IEnumerable<Product>));

            var service = _autoMocker.CreateInstance<ProductService>();
            var productRequest = new ProductRequestDTO() { MaxPrice = 25 };

            //Act
            var result = await service.GetProductsResponse(productRequest,default);

            //Assert
            Assert.Equal(expected, result.Filter.CommonWords);
        }
    }

}
