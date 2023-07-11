using Poq.Application.Helpers;
using Poq.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Poq.UnitTests.Helpers
{
    public class FilterHelperTests
    {
        [Theory]
        [InlineData(0,20,"small")]
        [InlineData(5,15,"large")]
        public void FilterProduct_Valid_ReturnsCorrectBool(float minPrice, float maxPrice, string size)
        {
            //Arrange
            var product = new Product("product0", 10, new List<string> { "small", "large" }, "test product");

            var productRequest = new ProductRequestDTO()
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Size = size
            };

            //Act
            var result = FilterHelper.FilterProduct(productRequest, product);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void CreateFilter_Valid_ReturnsProductFilter()
        {
            //Arrange
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
                new Product("product10", 1, new List<string>{ "medium","large" }, "test product for common words jacket brown"),
                new Product("product11", 11, new List<string>{ "large" }, "test product for common words boots green"),
                new Product("product12", 15, new List<string>{ "medium" }, "test product for common words trouser white"),
                new Product("product13", 10, new List<string>{ "small","large" }, "test product for common words boots black"),
                new Product("product14", 5, new List<string>{ "medium","large" }, "test product for common words shirt red"),
                new Product("product15", 11, new List<string>{ "large" }, "test product for common words jacket black"),
                new Product("product16", 20, new List<string>{ "medium" }, "test product for common words shirt green"),
            };

            var keys = new List<string> { "trouser", "red", "green", "boots", "shirt", "black", "socks", "white",
                                          "jacket", "towel","trouser1", "red1", "green1", "boots1", "shirt1" };

            var commonWords = new Dictionary<string, int>();
            foreach (var key in keys)
            {
                commonWords.Add(key, 5);
            }
            var mostCommonWords = commonWords.Keys.Skip(5).Take(10);
            var sizes = new List<string>() { "small", "medium", "large" };

            //Act
            var result = FilterHelper.CreateFilter(products, mostCommonWords, sizes);

            //Assert
            Assert.True(result.MaxPrice == 20);
            Assert.True(result.MinPrice == 1);
            Assert.True(result.CommonWords.Count() == 10);
        }
    }
}
