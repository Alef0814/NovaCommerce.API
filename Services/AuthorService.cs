using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using NovaCommerce.API.Data;
using NovaCommerce.API.DTOs;
using NovaCommerce.API.Models;
using NovaCommerce.API.Services.Interfaces;


namespace NovaCommerce.API.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AuthorService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<List<AuthorResponseDTO>> GetAllAsync()
        {
            return await _context.Authors
                .ProjectTo<AuthorResponseDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        // GET BY ID
        public async Task<AuthorResponseDTO?> GetByIdAsync(int id)
        {
            return await _context.Authors
                .Where(a => a.Id == id)
                .ProjectTo<AuthorResponseDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
        }

        // CREATE
        public async Task<AuthorResponseDTO> CreateAsync(AuthorCreateDTO dto, string userId)
        {
            var author = _mapper.Map<Author>(dto);

            // Correção importante: você estava tentando atribuir a uma propriedade estática ou inexistente
            author.CreatedById = userId; // Assumindo que sua entidade Author tem essa propriedade
            author.CreatedAt = DateTime.UtcNow; // Boa prática: definir data de criação

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorResponseDTO>(author);
        }

        // UPDATE
        public async Task<AuthorResponseDTO?> UpdateAsync(int id, AuthorCreateDTO dto, string userId)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return null;

            // Mapeia os campos do DTO para a entidade existente
            _mapper.Map(dto, author);

            // Opcional: atualizar quem modificou e quando
            author.UpdatedById = userId;
            author.UpdatedAt = DateTime.UtcNow;

            // EF Core detecta mudanças automaticamente, mas SaveChanges é necessário
            await _context.SaveChangesAsync();

            return _mapper.Map<AuthorResponseDTO>(author);
        }

        // Recomendado: Adicionar DELETE também (opcional)
        public async Task<bool> DeleteAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (author == null)
                return false;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}