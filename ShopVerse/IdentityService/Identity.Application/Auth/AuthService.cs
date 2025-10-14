using Identity.Application.Dtos;
using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Auth;

public class AuthService : IAuthService
{
    
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUnitOfWork unitOfWork,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task AssignRoleAsync(AssignRoleDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email)
            ?? throw new Exception("User not found.");

        var role = await _roleRepository.GetByNameAsync(dto.RoleName)
            ?? throw new Exception("Role not found.");

        user.AddRole(role.Id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<AuthResultDto> LoginAsync(LoginRequestDto request)
    {
        var existUser = await _userRepository.GetByEmailAsync(request.Email);

        if (existUser == null || existUser.PasswordHash != request.Password)
            throw new ArgumentException("Invalid email or password.");

        var roles = existUser.UserRoles.Select(ur => ur.Role.Name).ToList();
       
        var token = _jwtTokenGenerator.GenerateToken(existUser, roles);

        return new AuthResultDto
        {
            Email = existUser.Email,
            Token = token
        };
    }


    public async Task<AuthResultDto> RegisterAsync(RegisterRequestDto request)
    {
        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
            throw new ArgumentException("User already exists.");

        var defaultRole = await _roleRepository.GetByNameAsync("User");
        if (defaultRole is null)
            throw new InvalidOperationException("Default role not found.");

        var user = User.Create(request.Email, request.Password);

        user.AddRole(defaultRole.Id); // cleaner if you update the method

        await _userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var roles = new List<string> { defaultRole.Name };
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        return new AuthResultDto
        {
            Email = user.Email,
            Token = token
        };
    }

}
