namespace movecareAPI.Models;

public class UsersModels
{
    public int userId { get; set; } = 0;
    public string username { get; set; }
    public string name { get; set; }
    public string surname { get; set; }
    public string email { get; set; }
    public string phoneNumber { get; set; }
    public string password { get; set; }
    public string passwordAgain { get; set; }
    public int isActive { get; set; } = 1;
    public DateTime createdOn { get; set; } = DateTime.Now;
    
}