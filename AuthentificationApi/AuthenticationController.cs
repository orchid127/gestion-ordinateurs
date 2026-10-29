using System.Reflection.Metadata;
using GestionOrdinateurs.Shared;
using Microsoft.AspNetCore.Mvc;
using GestionOrdinateurs.Shared;

namespace AuthentificationApi;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("login")]
    public IActionResult GenerationTokenJWT(RoleEnum role, string login)
    {
        var jwtKey = _configuration["Jwt:Key"] ?? "";
        var key = Encoding.ASCII.GetBytes(jwtKey);

        return Ok(GenTokenJWT(key, role, userId ?? "deulat_user_id"));
    }

    private string GenTokenJWT(byte[] keyP, RoleEnum role, string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var claims = new[] {
            new Claim(ClaimTypes.Name, userId),
            new Claim(ClaimTypes.Role, role.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyP), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }
}