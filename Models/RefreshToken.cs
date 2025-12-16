
namespace NovaCommerce.API.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }

        public string Token { get; set;} = null!;
        public DateTime Expires { get; set; } //Quando expira 

        public DateTime Created { get; set; } = DateTime.UtcNow; 
    
        public DateTime? Revoked { get; set; } //Null ativo  
        public string? ReplaceByToken { get; set; } //Pra rotação 

        //Se expirou ou foi revogado
        public bool IsExperired => DateTime.UtcNow >= Expires;
        public bool IsRevoked  => Revoked !=null;
        public bool IsActive =>Revoked == null && !IsExperired;

        // Chave estrangeira 
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}