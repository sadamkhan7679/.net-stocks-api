using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MinLength(1, ErrorMessage = "Symbol must be at least 1 character long")]
    [MaxLength(10, ErrorMessage = "Symbol must be less than 10 characters long")]
    public string Symbol { get; set; } = string.Empty;
    
    [Required]
    [MinLength(1, ErrorMessage = "CompanyName must be at least 1 character long")]
    [MaxLength(256, ErrorMessage = "CompanyName must be less than 256 characters long")]
    public string CompanyName { get; set; } = string.Empty;
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Purchase must be a positive number")]
    public decimal Purchase { get; set; }
    
    [Required]
    [Range(0.001, 100, ErrorMessage = "LastDiv must be a positive number")]
    public decimal LastDiv { get; set; }
    
    [Required]
    public string Industry { get; set; } = string.Empty;
    
    [Required]
    [Range(0, long.MaxValue, ErrorMessage = "MarketCap must be a positive number")]
    public long MarketCap { get; set; } = 0;
}