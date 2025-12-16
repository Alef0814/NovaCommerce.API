using System.ComponentModel.DataAnnotations;

namespace NovaCommerce.API.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}