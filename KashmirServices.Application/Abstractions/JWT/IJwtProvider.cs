using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Abstractions.JWT;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}
