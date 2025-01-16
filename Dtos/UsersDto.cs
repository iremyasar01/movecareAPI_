namespace movecareAPI.Dtos;

public class UsersDto
{
    public string username { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string password { get; set; }
    
    public string passwordAgain { get; set; }
}