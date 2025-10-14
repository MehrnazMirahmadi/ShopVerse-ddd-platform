using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IRoleRepository
{
    Task<Role?> GetByNameAsync(string roleName);
}

