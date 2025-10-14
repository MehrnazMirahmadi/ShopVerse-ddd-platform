using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class UnitOfWork(OrderDbContext context
    , IOrderRepository orderRepository)
    : IUnitOfWork, IDisposable
{

    private IDbContextTransaction? _transaction;

    public IOrderRepository OrderRepository => orderRepository;

    public bool HasActiveTransaction => _transaction != null;

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await context.Database.BeginTransactionAsync();
    }

    public void Commit()
    {
        _transaction?.Commit();
    }

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public IDbContextTransaction? GetCurrentTransaction()
    {
        return _transaction;
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();
    }
}
