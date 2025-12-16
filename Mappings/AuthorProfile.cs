using AutoMapper;
using NovaCommerce.API.Models;
using NovaCommerce.API.DTOs;   // ← esse é o caminho exato que você tem

namespace NovaCommerce.API.Mappings
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorResponseDTO>();
            CreateMap<AuthorCreateDTO, Author>();
            CreateMap<Author, AuthorCreateDTO>();
        }
    }
}