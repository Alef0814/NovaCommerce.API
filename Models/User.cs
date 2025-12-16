using System.ComponentModel.DataAnnotations;

namespace NovaCommerce.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength (60)]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string Role { get; set; } = "User";

        public List<RefreshToken> RefreshTokens  { get; set; } = new();
    }
}