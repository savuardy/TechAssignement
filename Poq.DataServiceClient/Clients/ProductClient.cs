using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Poq.Application.Interfaces;
using Poq.Application.Models;
using Poq.DataSourceClient.Configurations;
using Poq.DataSourceClient.Exceptions;
using Poq.DataSourceClient.Models;
using System.Net.Http.Headers;

namespace Poq.DataSourceClient.Clients
{
    public class ProductClient : IProductClient
    {
        private readonly MockyConfiguration _apiConfiguration;
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductClient> _logger;
        private readonly IMapper _mapper;

        public ProductClient(
            IOptions<MockyConfiguration> apiConfiguration,
            HttpClient httpClient,
            ILogger<ProductClient> logger,
            IMapper mapper)
        {
            ArgumentNullException.ThrowIfNull(apiConfiguration);
            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(logger);
            ArgumentNullException.ThrowIfNull(mapper);

            _apiConfiguration = apiConfiguration.Value;
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token)
        {
            var result = await GetAsync(token);

            if (result == null)
            {
                _logger.LogError("Endpoint is providing no response");
                throw new MockyUnavailableException("No response from mocky");
            }

            var mappedresult = _mapper.Map<IEnumerable<MockyProductResponse>, IEnumerable<Product>>(result.Products);

            return mappedresult;
        }

        private async Task<MockyProductsResponse> GetAsync(CancellationToken token)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var uri = GetUrlFromConfiguration(_apiConfiguration);

            var response = await _httpClient.GetAsync(uri, token);

            var result = await ResponseHandlerAsync(response);

            return result;
        }

        private string GetUrlFromConfiguration(MockyConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration.ApiKey))
            {
                _logger.LogError("ProductsEndpoint for Mocky is not initialized");
                throw new EndpointIncorrectException("ProductsEndpoint for Mocky is not initialized");
            }

            return configuration.BaseUrl + configuration.ApiKey;
        }

        private async Task<MockyProductsResponse> ResponseHandlerAsync(HttpResponseMessage response)
        {
            try
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = new List<JsonConverter>
                    {
                        new StringEnumConverter()
                    }
                };

                return string.IsNullOrWhiteSpace(responseContent)
                    ? null
                    : JsonConvert.DeserializeObject<MockyProductsResponse>(responseContent, jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while achieving Response from Mocky");
                throw new MockyUnavailableException("Error while achieving products, see inner exception", ex);
            }
        }
    }
}
