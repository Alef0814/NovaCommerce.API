using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NovaCommerce.API.Data;
using NovaCommerce.API.DTOs;
using NovaCommerce.API.Models;
using NovaCommerce.API.Services.Interfaces;

namespace NovaCommerce.API.Services.Interfaces
{
        public class ProductService : IProductService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return _mapper.Map<List<ProductDTO>>(products);    
        }

        public async Task<ProductDTO?>GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product ==  null ? null : _mapper.Map<ProductDTO>(product); 
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO dto)
        {
            var product = _mapper.Map<Product>(dto);

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool>UpdateAsync(int id, UpdateProductDTO dto)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
                return false;

            _mapper.Map(dto, product);

            await _context.SaveChangesAsync();
            return true;    
        }

        public async Task<bool>DeleteAsync (int id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
                return false;   



            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
                return true;

        }

        Task<ProductDTO?> IProductService.UpdateAsync(int id, UpdateProductDTO dto)
        {
            throw new NotImplementedException();
        }
    } 
}

