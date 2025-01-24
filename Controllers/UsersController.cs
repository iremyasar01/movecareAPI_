using Microsoft.AspNetCore.Mvc;
using movecareAPI.Data;
using movecareAPI.Dtos;
using movecareAPI.Models;

namespace movecareAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UsersController : ControllerBase
{
    private readonly MoveDbContext _context;

    public UsersController(MoveDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [Route("Registration")]
    public IActionResult Registration(UsersDto usersDto)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { Errors = errors });
        }
      
        var objUser = _context.Users.FirstOrDefault(x =>
            x.email == usersDto.email || x.phoneNumber == usersDto.phoneNumber || x.username == usersDto.username);
        if (objUser == null)
        {
            _context.Users.Add(new UsersModels
            {
                username = usersDto.username,
                name = usersDto.name,
                surname = usersDto.surname,
                email = usersDto.email,
                phoneNumber = usersDto.phoneNumber,
                password = usersDto.password,
                passwordAgain = usersDto.passwordAgain,
            });
            _context.SaveChanges();
            return Ok("User registered succesfully");
        }
        else
        {
            return BadRequest("User already exist");
        }
    }
    [HttpPost]
    [Route("Login")]
    public IActionResult Login(LoginDto loginDto)
    {
        // Kullanıcının hem email hem username'i boş girmemesi kontrolü
        if (string.IsNullOrEmpty(loginDto.Email) && string.IsNullOrEmpty(loginDto.Username))
        {
            return BadRequest("You must provide either an email or a username.");
        }

        // Kullanıcı email veya username ile giriş yapabilir
        var user = _context.Users.FirstOrDefault(x =>
            (x.email == loginDto.Email || x.username == loginDto.Username) &&
            x.password == loginDto.Password && 
            x.passwordAgain == loginDto.PasswordAgain);

        if (user != null)
        {
            return Ok(user);
        }

        return NoContent();
    }


    [HttpGet]
    [Route("GetUsers")]
    public IActionResult GetUsers()
    {
        return Ok(_context.Users.ToList());
    }

    [HttpGet]
    [Route("GetUser")]
    public IActionResult GetUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.userId == id);
        if (user != null)
            return Ok(user);
        else
        {
            return NoContent();
        }
    }
    [HttpPost]
    [Route("UpdatePassword")]
    public IActionResult UpdatePassword(UpdatePassDto updatePasswordDto)
    {
        // 1. Model validation
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { Errors = errors });
        }

        // 2. Kullanıcıyı kontrol et
        var user = _context.Users.FirstOrDefault(x => x.userId == updatePasswordDto.UserId);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        // 3. Mevcut şifreyi doğrula
        if (user.password != updatePasswordDto.CurrentPassword)
        {
            return BadRequest("The current password is incorrect.");
        }

        // 4. Yeni şifre ile doğrulama şifresinin eşleştiğinden emin ol
        if (updatePasswordDto.NewPassword != updatePasswordDto.ConfirmNewPassword)
        {
            return BadRequest("The new password and confirmation password do not match.");
        }

        // 5. Şifreyi güncelle
        user.password = updatePasswordDto.NewPassword;
        user.passwordAgain = updatePasswordDto.NewPassword; // passwordAgain de güncelleniyor

        _context.SaveChanges(); // Veritabanına kaydet

        return Ok("Password updated successfully.");
    }



}

