using Poq.Application.Models;

namespace Poq.Application.Helpers
{
    public static class FilterHelper
    {
        public static bool FilterProduct(ProductRequestDTO model, Product product)
        {
            if (model.MinPrice.HasValue && product.Price < model.MinPrice)
            {
                return false;
            }

            if (model.MaxPrice.HasValue && product.Price > model.MaxPrice)
            {
                return false;
            }

            if (model.Size != null && product.Sizes != null && !product.Sizes.Contains(model.Size))
            {
                return false;
            }

            return true;
        }

        public static ProductFilter CreateFilter(IEnumerable<Product> products, IEnumerable<string> mostCommonWords, IEnumerable<string> sizes)
        {
            var maxPrice = products.Max(x => x.Price);
            var minPrice = products.Min(x => x.Price);
            
            return new ProductFilter(minPrice, maxPrice, mostCommonWords, sizes);
        }
    }
}
