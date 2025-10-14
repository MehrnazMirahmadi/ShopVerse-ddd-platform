using Catalog.Domain.Contract;
using Catalog.Domain.Contract.Repositories;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Catalog.Infrastructure.Persistence;

public class UnitOfWork(CatalogDbContext context
    , IProductRepository productRepository
    ,ICategoryRepository categoryRepository)
    : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _transaction;
    public IProductRepository ProductRepository => productRepository;
    public ICategoryRepository CategoryRepository => categoryRepository;
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
