using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Account;

public class RegisterDto
{
    
    [Required]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    public string UserName { get; set; } = String.Empty;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
    
    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = String.Empty;
    
    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = String.Empty;
    
}