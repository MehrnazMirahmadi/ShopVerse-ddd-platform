using Catalog.Domain.Contract.Repositories;

namespace Catalog.Domain.Contract;

public interface IUnitOfWork
{
    IProductRepository ProductRepository { get; }
    ICategoryRepository CategoryRepository { get; }
    //----------------------------------------------------------------
    bool HasActiveTransaction { get; }
    Task BeginTransactionAsync();
    void Commit();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}

