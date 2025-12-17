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
var jwtConfig = builder.Configuration.GetSection("Jwt");
if (string.IsNullOrEmpty(jwtConfig["Key"]) || jwtConfig["Key"]!.Length < 32)
    throw new InvalidOperationException("JWT Key deve ter no mínimo 256 bits (32 caracteres).");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "NovaCommerce API", Version = "v1" });
    
    // Adiciona suporte ao Bearer no Swagger (opcional, mas fica lindo)
    c.AddSecurityDefinition("Bearer", new()
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new()
    {
        {
            new() {
                Reference = new() { Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// === DATABASE ===
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));



// === AUTO MAPPER ===
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// === SERVICES ===
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();




// === JWT AUTHENTICATION ===
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.FromMinutes(5), // tolerância comum
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!))
        };

        // Eventos úteis para debug (opcional)
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                    context.Response.Headers.Append("Access-Control-Allow-Origin", "*");

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// === MIDDLEWARE PIPELINE ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NovaCommerce API v1"));
}

app.UseHttpsRedirection();

// Ordem crítica!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();