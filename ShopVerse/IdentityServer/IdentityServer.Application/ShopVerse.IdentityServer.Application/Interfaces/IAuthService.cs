using ShopVerse.IdentityServer.Application.Dtos;

namespace ShopVerse.IdentityServer.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterUserDto registerUserDto);
    Task<AuthResultDto> LoginAsync(LoginUserDto dto);
}
