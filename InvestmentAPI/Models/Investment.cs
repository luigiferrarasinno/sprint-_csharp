using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentAPI.Models
{
    public class Investment
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // Ex: Ação, Tesouro, CDB, etc.
        
        public decimal Amount { get; set; }
        
        public decimal? ExpectedReturn { get; set; } // Percentual de retorno esperado
        
        public DateTime InvestmentDate { get; set; } = DateTime.UtcNow;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        // Foreign key
        public int UserId { get; set; }
        
        // Navigation property
        public virtual User? User { get; set; }
    }
}
