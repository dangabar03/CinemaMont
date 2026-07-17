using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinemaMont.Models;
using Microsoft.IdentityModel.Tokens;

namespace CinemaMont.Authentication;

public class JwtToken
{
    private readonly IConfiguration _config;
    private readonly string _keySecret;
    private readonly int _minutes;

    public JwtToken(IConfiguration config)
    {
        _config = config;
        _keySecret = config["Jwt:Key"]!;
        _minutes = config.GetValue<int>("Jwt:ExpiryMinutes", 120);
    }

    public string GenerateToken(User user)
    {

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Type.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_keySecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_minutes),
            signingCredentials: cred
        );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}