namespace Poq.Application.Models
{
    public class ProductRequestDTO
    {
        public float? MaxPrice { get; set; }
        public float? MinPrice { get; set; }
        public string? Size { get; set; }
        public string? Highlight { get; set; }
    }
}
