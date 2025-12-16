using AutoMapper;
using NovaCommerce.API.Models;
using NovaCommerce.API.DTOs;

namespace NovaCommerce.API.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            // se precisar do inverso depois:
            // CreateMap<ProductDTO, Product>();
        }
    }
}
