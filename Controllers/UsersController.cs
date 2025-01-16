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
    public IActionResult Login (LoginDto loginDto)
    {
        var user = _context.Users.FirstOrDefault(x => x.email == loginDto.email || x.username == loginDto.username &&
            x.password == loginDto.password && x.passwordAgain == loginDto.passwordAgain);
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
            return Ok();
        else
        {
            return NoContent();
        }
    }

}

