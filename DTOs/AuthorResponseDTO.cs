
namespace NovaCommerce.API.DTOs
{
    public class AuthorResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string  Bio { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
    }
}