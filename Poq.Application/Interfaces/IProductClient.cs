using Poq.Application.Models;

namespace Poq.Application.Interfaces
{
    public interface IProductClient
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken token);
    }
}