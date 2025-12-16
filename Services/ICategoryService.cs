using NovaCommerce.API.Models;

namespace NovaCommerce.API.Services
{
    public interface ICategoryService
    {
        Task< IEnumerable<CategoryDTO>>GetAllAsync();
        Task<CategoryDTO?>GetByIdAsync(int id);
        Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}