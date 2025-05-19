namespace Infrastructure.Persistence.Context;

public class RepositoryPatternDbContext : DbContext
{
    public RepositoryPatternDbContext(DbContextOptions<RepositoryPatternDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryPatternDbContext).Assembly);
    }

    public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
}
