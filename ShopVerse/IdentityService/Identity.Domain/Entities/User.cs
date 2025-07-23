using ShopVerse.BuildingBlocks.Abstractions;

namespace Identity.Domain.Entities;

public class User : Aggregate<Guid> 
{
    public string Email { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;

    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    private User() { } // For EF

    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
    public static User Create(string email, string passwordHash)
    {
        return new User(email, passwordHash);
    }

    public void AddRole(Guid roleId)
    {
        UserRoles.Add(new UserRole(Id, roleId));
    }
}
