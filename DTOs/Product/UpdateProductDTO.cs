

namespace NovaCommerce.API.DTOs
{
    public class UpdateProductDTO
    {
        public string Name { get; set;} = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}