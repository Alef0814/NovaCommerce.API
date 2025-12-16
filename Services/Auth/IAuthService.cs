

using NovaCommerce.API.DTOs.Auth;

namespace NovaCommerce.API.Services.Auth
{
    public interface IAuthService
    {
        Task<TokenResponseDTO>RegisterAsync(RegisterDTO dTO);
        Task<TokenResponseDTO?>LoginAsync(LoginDTO dTO);
        Task<TokenResponseDTO?>RefreshTokenAsync(string Token);
        Task<bool> RevokeTokenAsync(string Token);
    }
}