using Domain.Contract.Repositories;

namespace Domain.Contract;

public interface IUnitOfWork
{
    IOrderRepository OrderRepository { get; }
    //----------------------------------------------------------------
    bool HasActiveTransaction { get; }
    Task BeginTransactionAsync();
    void Commit();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
