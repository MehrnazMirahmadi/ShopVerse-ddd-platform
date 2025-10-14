using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, List<string> roles);
}
