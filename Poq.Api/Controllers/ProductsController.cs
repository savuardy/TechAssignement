using Microsoft.AspNetCore.Mvc;
using Poq.Api.Models;
using Poq.Application.Interfaces;
using Poq.Application.Models;

namespace Poq.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductService productService, ILogger<ProductsController> logger)
        {
            ArgumentNullException.ThrowIfNull(productService);
            ArgumentNullException.ThrowIfNull(logger);

            _productService = productService;
            _logger = logger;
        }

        /// <summary>
        /// Returns filtered collection of products.
        /// </summary>
        /// <param name="requestModel">Data of filter</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Filtered products and all available products statistics</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetAsync([FromQuery] ProductRequest requestModel, CancellationToken token)
        {
            if(requestModel.MinPrice > requestModel.MaxPrice)
            {
                _logger.LogInformation("Specified arguments were wrong MinPrice can't be higher than MaxPrice");
                throw new ArgumentException("MaxPrice");
            }

            var requestModelDto = new ProductRequestDTO
            {
                Size = requestModel.Size,
                Highlight = requestModel.Highlight,
                MaxPrice = requestModel.MaxPrice,
                MinPrice = requestModel.MinPrice
            };
            var res = await _productService.GetProductsResponse(requestModelDto, token);
            return Ok(res);
        }
    }
}
