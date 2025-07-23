using ShopVerse.BuildingBlocks.Abstractions;

namespace Identity.Domain.Entities;

public class UserRole : Entity<Guid>
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = default!;

    public Guid RoleId { get; private set; }
    public Role Role { get; private set; } = default!;

    private UserRole() { }

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}
