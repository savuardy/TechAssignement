
using Poq.Application.Models;

namespace Poq.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponseDTO> GetProductsResponse(ProductRequestDTO model, CancellationToken token);
    }
}
