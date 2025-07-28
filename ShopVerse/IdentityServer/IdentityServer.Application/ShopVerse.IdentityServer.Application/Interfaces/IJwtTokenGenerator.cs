using ShopVerse.IdentityServer.Application.Dtos;

namespace ShopVerse.IdentityServer.Application.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(UserModel user);
}
