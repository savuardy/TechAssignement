using Poq.Application.Helpers;
using Poq.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poq.UnitTests.Helpers
{
    public class StatisticsCollectorTests
    {
        [Fact]
        public void CollectUniqueSizes_Valid_ReturnsCorrectSizes()
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

            //Act
            var result = StatisticsCollector.CollectUniqueSizes(products);

            //Assert
            Assert.True(result.Count() == 3);
        }

        [Fact]
        public void CollectCommonDescriptionWords_Valid_ReturnsProductFilter()
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

            //Act
            var result = StatisticsCollector.CollectCommonDescriptionWords(products);

            //Assert
            Assert.True(result.Keys.Count() == 4);
        }
    }
}
