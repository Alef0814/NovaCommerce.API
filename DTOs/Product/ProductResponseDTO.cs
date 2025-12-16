

namespace NovaCommerce.API.DTOs.Product
{
    public class ProductResponseDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Decimal Price { get; set; }
        public int stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}