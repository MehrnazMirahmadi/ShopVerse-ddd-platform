using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ShopVerse.IdentityServer.Application.Dtos;
using ShopVerse.IdentityServer.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShopVerse.IdentityServer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto, [FromServices] IValidator<RegisterUserDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var result = await _authService.RegisterAsync(dto);
        return Ok(result);
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto, [FromServices] IValidator<LoginUserDto> validator)
    {
        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }
}
