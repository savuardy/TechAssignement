namespace Poq.Api.Models
{
    public class ProductRequest
    {
        /// <summary>
        /// Minimal price of result elements.
        /// </summary>
        /// <example>12</example>
        public float? MaxPrice { get; set; }
        /// <summary>
        /// Maximal price of result elements.
        /// </summary>
        /// <example>17</example>
        public float? MinPrice { get; set; }
        /// <summary>
        /// Size which is required in result elements.
        /// </summary>
        /// <example>small</example>
        public string? Size { get; set; }
        /// <summary>
        /// Words to highlight.
        /// </summary>
        /// <example>small, large</example>
        public string? Highlight { get; set; }
    }
}
