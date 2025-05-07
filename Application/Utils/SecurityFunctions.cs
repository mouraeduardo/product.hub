using Domain.Models;
using Domain.Models.ConfigurationModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Utils;

public class SecurityFunctions 
{
    private readonly JwtSettings _jwtSettings;
    private readonly HashSettings _hashSettings;

    public SecurityFunctions(IOptions<JwtSettings> jwtSettings, IOptions<HashSettings> hashSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _hashSettings = hashSettings.Value;
    }

    public string GenerateJwtToken(User user) 
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    public string GenerateSalt() 
    {
        using var rng = RandomNumberGenerator.Create();
        var byteSalt = new byte[16];
        rng.GetBytes(byteSalt);
        var salt = Convert.ToBase64String(byteSalt);
        return salt;
    }

    public string ComputePasswordHash(string password, string salt) 
    {
        using var sha256 = SHA256.Create();
        var combined = Encoding.UTF8.GetBytes($"{password}{salt}{_hashSettings.Pepper}");
        var hash = sha256.ComputeHash(combined);

        for (int i = 1; i < _hashSettings.Iterations; i++) 
        {
            hash = sha256.ComputeHash(hash);
        }

        return Convert.ToBase64String(hash);

    }
}
