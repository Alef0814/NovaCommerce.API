using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NovaCommerce.API.Data;
using NovaCommerce.API.DTOs;
using NovaCommerce.API.Models;


namespace NovaCommerce.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IMapper  _mapper; 

        public CategoryService (DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    
        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryDTO?>(category);

        }

        public async Task<CategoryDTO> CreateAsync(CategoryCreateDTO dTO)
        {
            var category = _mapper.Map<Category>(dTO);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool>DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            return false;

            await _context.SaveChangesAsync();
            return true;
        }



    }
}

