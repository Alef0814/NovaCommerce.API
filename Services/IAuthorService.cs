using NovaCommerce.API.DTOs; // ou NovaCommerce.API.DTOs.Author se for subpasta

namespace NovaCommerce.API.Services.Interfaces // ou o namespace que você usa
{
    public interface IAuthorService
    {
        Task<List<AuthorResponseDTO>> GetAllAsync();
        Task<AuthorResponseDTO?> GetByIdAsync(int id);
        Task<AuthorResponseDTO> CreateAsync(AuthorCreateDTO dto, string userId);
        Task<AuthorResponseDTO?> UpdateAsync(int id, AuthorCreateDTO dto, string userId);
        Task<bool> DeleteAsync(int id);
    }
}