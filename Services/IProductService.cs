using NovaCommerce.API.DTOs;
using NovaCommerce.API.Services.Interfaces;

namespace NovaCommerce.API.Services.Interfaces
{
        public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task< ProductDTO?> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task< ProductDTO?> UpdateAsync(int id, UpdateProductDTO dto );
        Task <bool> DeleteAsync( int id);
    }

}
    
