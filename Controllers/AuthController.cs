using atc_backend_app.Models;
using atc_backend_app.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserDto userDto)
    {
        var response = await _userService.RegisterAsync(userDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto userDto)
    {
        var response = await _userService.LoginAsync(userDto);
        if (!response.Success)
        {
            return BadRequest(response.Message);
        }
        return Ok(new { Token = response.Token });
    }
}
