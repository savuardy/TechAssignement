namespace Poq.Application.Models
{
    public record ProductResponseDTO(IEnumerable<Product> Products, ProductFilter Filter);
}
