using System.ComponentModel.DataAnnotations;

namespace NovaCommerce.API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // LISTA DE PRODUTOS 
        public List <Product> Products  { get; set; } = null!;
    }
}