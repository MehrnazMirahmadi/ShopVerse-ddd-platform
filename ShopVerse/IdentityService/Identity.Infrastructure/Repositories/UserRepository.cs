using Identity.Application.Interfaces;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories;

public class UserRepository (IdentityDbContext identityDbContext)
    : IUserRepository
{
    
    public async Task AddAsync(User user)
    {
        await identityDbContext.Users.AddAsync(user);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await identityDbContext.Users
             .Include(u => u.UserRoles)
                 .ThenInclude(ur => ur.Role)
             .FirstOrDefaultAsync(u => u.Email == email);
    }
}
