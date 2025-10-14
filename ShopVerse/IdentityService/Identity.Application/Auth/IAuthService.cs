using Identity.Application.Dtos;

namespace Identity.Application.Auth;

public interface IAuthService
{
    Task<AuthResultDto> RegisterAsync(RegisterRequestDto request);
    Task<AuthResultDto> LoginAsync(LoginRequestDto request);
    Task AssignRoleAsync(AssignRoleDto dto);
}