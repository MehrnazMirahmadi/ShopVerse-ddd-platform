using ShopVerse.BuildingBlocks.Abstractions;

namespace Identity.Domain.Entities;

public class Role : Aggregate<Guid>  
{
    public string Name { get; private set; } = default!;
    public ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();

    protected Role() { }

    public Role(string name) => Name = name;
}
