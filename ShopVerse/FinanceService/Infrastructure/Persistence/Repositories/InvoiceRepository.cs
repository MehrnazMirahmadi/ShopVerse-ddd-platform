namespace Finance.Infrastructure.Persistence.Repositories;

public class InvoiceRepository(FinanceDbContext dbContext) : IInvoiceRepository
{
    public async Task AddAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        await dbContext.Invoices.AddAsync(invoice, cancellationToken);
    }
    public async Task<Invoice?> GetByIdAsync(long invoiceId, CancellationToken cancellationToken = default)
    {
        var invoice = await dbContext.Invoices
            .AsNoTracking()
            .Include(i => i.Payments)
            .FirstOrDefaultAsync(i => i.Id == invoiceId, cancellationToken);

        return invoice;
    }

    public Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default)
    {
        dbContext.Invoices.Update(invoice);
        return Task.CompletedTask;
    }
    public async Task<bool> ExistsAsync(long invoiceId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Invoices.AnyAsync(i => i.Id == invoiceId, cancellationToken);
    }

    public async Task<PaginationResult<Invoice>> GetAllAsync(BaseSpecification<Invoice> specification, CancellationToken cancellationToken = default)
    {
        var query = dbContext.Invoices.AsQueryable();
        if (specification.Criteria != null)
            query = query.Where(specification.Criteria);

        foreach (var include in specification.Includes)
            query = query.Include(include);

        if (specification.OrderBy != null)
            query = query.OrderBy(specification.OrderBy);
        else if (specification.OrderByDescending != null)
            query = query.OrderByDescending(specification.OrderByDescending);

        var totalCount = await query.CountAsync(cancellationToken);


        if (specification.Skip.HasValue && specification.Take.HasValue)
            query = query.Skip(specification.Skip.Value).Take(specification.Take.Value);

        var items = await query.AsNoTracking().ToListAsync(cancellationToken);
        return new PaginationResult<Invoice>(
           specification.Take ?? totalCount,
           specification.Skip.HasValue ? (specification.Skip.Value / (specification.Take ?? totalCount) + 1) : 1,
           totalCount,
           items);
    }

}