namespace api.DTOs.Account;

public class NewUserDto
{
    
    public string UserName { get; set; } = String.Empty;
    
    public string Email { get; set; } = String.Empty;
    
    public string Token { get; set; } = String.Empty;
}