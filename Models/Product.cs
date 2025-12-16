namespace NovaCommerce.API.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    
    // Chave Estrangeira

        public int CategoryId { get; set; }
    
        
    
    // Propiedade de Navegação Obrigatória
        public Category Category { get; set; } = null!;
    
    
    }
}

    