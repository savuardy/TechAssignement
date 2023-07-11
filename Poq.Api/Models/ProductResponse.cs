using Poq.Application.Models;

namespace Poq.Api.Models
{
    public record ProductResponse(IEnumerable<Product> Products, ProductFilter Filter);
}
