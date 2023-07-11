using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Poq.Application.Configurations;
using Poq.Application.Helpers;
using Poq.Application.Interfaces;
using Poq.Application.Models;

namespace Poq.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductClient _productClient;
        private readonly ILogger<ProductService> _logger;
        private readonly FilterLogicConfiguration _logicConfiguration;

        public ProductService(
            IProductClient productClient, ILogger<ProductService> logger, IOptions<FilterLogicConfiguration> logicConfiguration)
        {
            ArgumentNullException.ThrowIfNull(productClient);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(logicConfiguration);

            _productClient = productClient;
            _logger = logger;
            _logicConfiguration = logicConfiguration.Value;

        }

        public async Task<ProductResponseDTO> GetProductsResponse(ProductRequestDTO model, CancellationToken token)
        {
            var products = await GetAllProductsAsync(token);
            var result = FilterProducts(products, model);
            return result;
        }

        private ProductResponseDTO FilterProducts(IEnumerable<Product> products, ProductRequestDTO model)
        {
            var result = new List<Product>();
            ProductFilter filter = null;

            _logger.LogTrace("Started filtering products with parameters: MinPrice {MinPrice}, MaxPrice: {MaxPrice}, Size: {Size}, Highlight: {Highlight}",
                 model.MinPrice, model.MaxPrice, model.Size, model.Highlight);

            var mostCommonWords = StatisticsCollector.CollectCommonDescriptionWords(products)
                                                     .Keys.Skip(_logicConfiguration.SkipWords)
                                                     .Take(_logicConfiguration.TakeWords);
            var uniquesSizes = StatisticsCollector.CollectUniqueSizes(products);


            foreach (var product in products)
            {
                //check if product is acceptable
                if (FilterHelper.FilterProduct(model, product))
                {
                    if (!string.IsNullOrWhiteSpace(model.Highlight) && !string.IsNullOrWhiteSpace(product.Description))
                    {
                        product.Description = WordsHighlighter.HighlightDescription(product.Description,
                                                                                    model.Highlight, 
                                                                                    _logicConfiguration.Highlighter);
                    }
                    result.Add(product);
                }
            }

            _logger.LogTrace($"Finished filtering");

            if(model.MinPrice.HasValue || model.MaxPrice.HasValue || !string.IsNullOrWhiteSpace(model.Size) || !string.IsNullOrWhiteSpace(model.Highlight))
            { 
                filter = FilterHelper.CreateFilter(products, mostCommonWords, uniquesSizes);
            }

            return new ProductResponseDTO(result, filter);
        }

        private Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken token)
        {
            return _productClient.GetProductsAsync(token);
        }
    }
}
