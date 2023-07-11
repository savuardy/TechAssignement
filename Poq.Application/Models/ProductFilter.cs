namespace Poq.Application.Models
{
    public record ProductFilter(float? MinPrice, float? MaxPrice, IEnumerable<string> CommonWords, IEnumerable<string> Sizes);
}