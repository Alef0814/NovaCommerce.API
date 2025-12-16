using System;
using System.Security.Cryptography;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using NovaCommerce.API.Data;
using NovaCommerce.API.DTOs.Auth;
using NovaCommerce.API.Models;

namespace NovaCommerce.API.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly JwtService _jwtService;

        public AuthService(DataContext context, JwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        public async Task<TokenResponseDTO> RegisterAsync(RegisterDTO dto)
        {
            var existe = await _context.Users
                .AnyAsync(u => u.Username == dto.UserName || u.Email == dto.Email);

            if (existe)
                throw new InvalidOperationException("Usuário ou e-mail já cadastrado.");

            var user = new User
            {
                Username = dto.UserName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600
            };
        }

        public async Task<TokenResponseDTO> LoginAsync(LoginDTO dto)
        {
            // AQUI ESTAVA O PROBLEMA: seu LoginDTO tem "UserName", não "Username"
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == dto.UserName);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Usuário ou senha inválidos.");

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = 3600
            };
        }

        public async Task<TokenResponseDTO> RefreshTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.Token == token && rt.Expires > DateTime.UtcNow);

            if (refreshToken == null)
                throw new UnauthorizedAccessException("Refresh token inválido ou expirado.");

            var newAccessToken = _jwtService.GenerateAccessToken(refreshToken.User);

            return new TokenResponseDTO
            {
                AccessToken = newAccessToken,
                RefreshToken = token,
                ExpiresIn = 3600
            };
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null)
                return false;

            _context.RefreshTokens.Remove(refreshToken);
            await _context.SaveChangesAsync();
            return true;
        }

        private string GenerateRefreshToken()
        {
            var bytes = new byte[64];
            RandomNumberGenerator.Fill(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}