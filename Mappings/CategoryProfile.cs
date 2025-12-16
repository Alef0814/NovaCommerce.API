

using AutoMapper;
using NovaCommerce.API.Models;

namespace NovaCommerce.API.Mappings
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO , Category>();
            
        }
    }
}