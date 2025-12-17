using Microsoft.EntityFrameworkCore;
using NovaCommerce.API.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NovaCommerce.API.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Author> Authors=> Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // CATEGORY
    modelBuilder.Entity<Category>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity.HasIndex(e => e.Name).IsUnique();

        entity.HasData(
            new Category { Id = 1, Name = "Informática" },
            new Category { Id = 2, Name = "Periféricos" }
        );
    });

    // PRODUCT
    modelBuilder.Entity<Product>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        // ESSAS DUAS LINHAS FUNCIONAM EM TODAS AS VERSÕES DO EF CORE
        entity.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

        entity.Property(e => e.Stock)
            .HasDefaultValue(0);

        // Relacionamento
        entity.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        entity.HasIndex(e => e.Name);

        entity.HasData(
            new Product { Id = 1, Name = "Mouse Gamer", Price = 150.00m, Stock = 10, CategoryId = 1 },
            new Product { Id = 2, Name = "Teclado Mecânico", Price = 200.00m, Stock = 5, CategoryId = 2 },
            new Product { Id = 3, Name = "Monitor 24\"", Price = 900.00m, Stock = 7, CategoryId = 1 }
        );
    });

    // USER
    modelBuilder.Entity<User>(entity =>
    {
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(50);

        entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

        entity.Property(e => e.Role)
              .HasDefaultValue("User");   // ← FUNCIONA ASSIM MESMO!

        entity.HasIndex(e => e.Username).IsUnique();
        entity.HasIndex(e => e.Email).IsUnique();

        entity.HasMany(e => e.RefreshTokens)
            .WithOne(rt => rt.User)
            .HasForeignKey(rt => rt.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    });

    // REFRESH TOKEN
    modelBuilder.Entity<RefreshToken>(entity =>
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.Token).IsRequired();
        entity.Property(e => e.Expires).IsRequired();
        entity.HasIndex(e => e.Token);
    });
}

    
}
