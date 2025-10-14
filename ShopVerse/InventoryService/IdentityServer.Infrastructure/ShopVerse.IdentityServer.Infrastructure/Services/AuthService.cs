using Microsoft.AspNetCore.Identity;
using ShopVerse.IdentityServer.Application.Dtos;
using ShopVerse.IdentityServer.Application.Interfaces;
using ShopVerse.IdentityServer.Infrastructure.Identity;

namespace ShopVerse.IdentityServer.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResultDto> RegisterAsync(RegisterUserDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            //FirstName = dto.FirstName, 
            // LastName = dto.LastName,
            FullName = $"{dto.FirstName} {dto.LastName}"

        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception(errors);
        }

        var userModel = new UserModel
        {
            Id = Guid.Parse(user.Id), 
            Email = user.Email
        };


        var token = await _jwtTokenGenerator.GenerateTokenAsync(userModel);

        return new AuthResultDto
        {
            Token = token,
            ExpireAt = DateTime.UtcNow.AddMinutes(60) // Or get from config
        };
    }

    public async Task<AuthResultDto> LoginAsync(LoginUserDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            throw new Exception("Invalid email or password.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            throw new Exception("Invalid email or password.");

        var userModel = new UserModel
        {
            Id = Guid.Parse(user.Id), 
            Email = user.Email
        };


        var token = await _jwtTokenGenerator.GenerateTokenAsync(userModel);

        return new AuthResultDto
        {
            Token = token,
            ExpireAt = DateTime.UtcNow.AddMinutes(60) // Or get from config
        };
    }
}
