using Finance.Domain.Contract.Repositories;

namespace Finance.Domain.Contract;

public interface IUnitOfWork
{
    IInvoiceRepository InvoiceRepository { get; }


    bool HasActiveTransaction { get; }
    Task BeginTransactionAsync();
    void Commit();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
