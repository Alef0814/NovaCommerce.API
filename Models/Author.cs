

namespace NovaCommerce.API.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;

        // Auditoria
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? CreatedById { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedById { get; set; }
    }
}
