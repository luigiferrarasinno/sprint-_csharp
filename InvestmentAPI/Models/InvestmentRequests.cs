using System.ComponentModel.DataAnnotations;

namespace InvestmentAPI.Models
{
    public class CreateInvestmentRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Range(0, 100, ErrorMessage = "O retorno esperado deve estar entre 0 e 100%")]
        public decimal? ExpectedReturn { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
    
    public class UpdateInvestmentRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;
        
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero")]
        public decimal Amount { get; set; }
        
        [Range(0, 100, ErrorMessage = "O retorno esperado deve estar entre 0 e 100%")]
        public decimal? ExpectedReturn { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int UserId { get; set; }
    }
}
