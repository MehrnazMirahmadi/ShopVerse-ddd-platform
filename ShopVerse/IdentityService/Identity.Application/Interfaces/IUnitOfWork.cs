namespace Identity.Application.Interfaces;

public interface IUnitOfWork
{
  
    bool HasActiveTransaction { get; }
    Task BeginTransactionAsync();
    void Commit();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
