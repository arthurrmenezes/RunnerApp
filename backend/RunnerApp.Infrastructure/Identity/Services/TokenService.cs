using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RunnerApp.Infrastructure.Identity.Entities;

namespace RunnerApp.Infrastructure.Identity.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(ApplicationUser applicationUser)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email!)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:PrivateKey"]!));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var accessTokenExpirationMinutes = _configuration.GetValue<double>("JwtSettings:AccessTokenExpirationMinutes");

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(accessTokenExpirationMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string Token, DateTime Expiration) GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var refreshToken = Convert.ToBase64String(randomNumber);
        var refreshTokenExpirationDays = _configuration.GetValue<int>("JwtSettings:RefreshTokenExpirationDays");
        var expiration = DateTime.UtcNow.AddDays(refreshTokenExpirationDays);

        return (refreshToken, expiration);
    }
}
