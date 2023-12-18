using KashmirService.Infrastructure.Identity;
using KashmirServices.Application.Abstractions.JWT;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KashmirServices.Infrastructure.JWT;

internal sealed class JwtProvider : IJwtProvider
{
    private readonly IConfiguration configuration;
    private const string UserRole = nameof(UserRole);

    public JwtProvider(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(AppClaimTypes.UserId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(UserRole , Enum.GetName(typeof(UserRole), user.UserRole) ?? ""),
            }),
            Expires = DateTime.Now.AddHours(1),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)), SecurityAlgorithms.HmacSha256),
        };

        var handler = new JwtSecurityTokenHandler();
        var securityToken = handler.CreateToken(descriptor);

        return handler.WriteToken(securityToken);
    }
}
