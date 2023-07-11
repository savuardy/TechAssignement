using Poq.Application.Models;

namespace Poq.Application.Helpers
{
    public static class StatisticsCollector
    {
        public static IEnumerable<string> CollectUniqueSizes(IEnumerable<Product> products)
        {
            var result = new List<string>();
            foreach (var product in products)
            {
                if(product.Sizes == null)
                {
                    continue;
                }

                foreach (var size in product.Sizes)
                {
                    if (!result.Contains(size.ToLower()))
                        result.Add(size);
                }
            }

            return result;
        }

        public static Dictionary<string, int> CollectCommonDescriptionWords(IEnumerable<Product> products)
        {
            var result = new Dictionary<string, int>();

            foreach (var product in products)
            {
                if (string.IsNullOrWhiteSpace(product.Description))
                {
                    continue;
                }

                var words = product.Description.ToLower().Trim('.').Split(" ");

                foreach (var word in words)
                {
                    if (!result.TryGetValue(word, out var count))
                    {
                        result.Add(word, 1);
                    }
                    else
                    {
                        result[word] = count + 1;
                    }
                }
            }

            return result;
        }
    }
}
