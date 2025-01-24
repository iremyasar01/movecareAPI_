namespace movecareAPI.Dtos;

public class LoginDto
{
    public string? Email { get; set; } // Opsiyonel hale getirdik
    public string? Username { get; set; } // Opsiyonel hale getirdik
    public string Password { get; set; }
    public string PasswordAgain { get; set; }
}
