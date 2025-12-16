

namespace NovaCommerce.API.DTOs
{
    public class CreateProductDTO
    {
        
        public String Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}