namespace Domain.Contract;

public interface IUnitOfWork
{
    IInventoryItemRepository InventoryRepository { get; }
    //----------------------------------------------------------------
    public bool HasActiveTransaction { get; }
    public Task BeginTransactionAsync();
    public void Commit();
    public Task CommitAsync();
    public Task RollbackAsync();
    public Task SaveChangesAsync();
}
