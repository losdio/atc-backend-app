//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using atc_backend_app.Models;

//namespace atc_backend_app.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly IConfiguration _configuration;

//        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _configuration = configuration;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] User user)
//        {
//            var newUser = new IdentityUser { UserName = user.Username, Email = user.Email };
//            var result = await _userManager.CreateAsync(newUser, user.Password);

//            if (result.Succeeded)
//            {
//                return Ok(new { Message = "User registered successfully!" });
//            }

//            return BadRequest(result.Errors);
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] User u)
//        {
//            var user = await _userManager.FindByNameAsync(u.Username);
//            if (user != null && await _userManager.CheckPasswordAsync(user, u.Password))
//            {
//                var token = GenerateJwtToken(user);
//                return Ok(new { Token = token });
//            }

//            return Unauthorized("Invalid login attempt.");
//        }

//        private string GenerateJwtToken(IdentityUser user)
//        {
//            var claims = new[]
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _configuration["Jwt:Issuer"],
//                audience: _configuration["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(30),
//                signingCredentials: creds);

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
