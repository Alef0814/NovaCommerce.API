using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NovaCommerce.API.Data;
using NovaCommerce.API.Mappings;
using NovaCommerce.API.Services;
using NovaCommerce.API.Services.Auth;
using NovaCommerce.API.Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// === CONFIGURAÇÃO DE CONFIGURAÇÃO (appsettings) ===
// Garante que recarregue em desenvolvimento e lança exceção se faltar chave obrigatória
// Remova as linhas acima e coloque dentro do AddJwtBearer:
// === JWT AUTHENTICATION ===
// === JWT AUTHENTICATION ===
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Fallbacks seguros para tudo
        var jwtKey = builder.Configuration["Jwt:Key"] 
                     ?? "MinhaChaveSuperSecretaParaJWT1234567890Alef0814"; // 44 caracteres

        var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "NovaCommerceAPI";
        var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "NovaCommerceClients";

        if (jwtKey.Length < 32)
            throw new InvalidOperationException("JWT Key deve ter no mínimo 256 bits (32 caracteres).");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(5),
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddCors();
builder.Services.AddAuthorization();
builder.Services.AddCors();

// ... resto do código

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", () => Results.Ok("NovaCommerce API rodando! 🚀"));

var port = Environment.GetEnvironmentVariable("PORT") ?? "8000";
app.Run($"http://0.0.0.0:{port}");