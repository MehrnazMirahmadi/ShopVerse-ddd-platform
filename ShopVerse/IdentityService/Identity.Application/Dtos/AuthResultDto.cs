namespace Identity.Application.Dtos;

public class AuthResultDto
{
    public string Email { get; set; } = default!;
    public string Token { get; set; } = default!;
}
