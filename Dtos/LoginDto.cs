namespace movecareAPI.Dtos;

public class LoginDto
{
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string passwordAgain { get; set; }
}