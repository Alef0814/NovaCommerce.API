

namespace NovaCommerce.API.DTOs.Auth
{
    public class LoginDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}