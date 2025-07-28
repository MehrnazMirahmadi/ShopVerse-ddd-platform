namespace ShopVerse.IdentityServer.Application.Dtos;

public class AuthResultDto
{
    public string Token { get; set; }
    public DateTime ExpireAt { get; set; }
}
