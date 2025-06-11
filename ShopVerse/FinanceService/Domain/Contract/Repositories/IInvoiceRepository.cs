namespace Finance.Domain.Contract.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(long invoiceId, CancellationToken cancellationToken = default);
    Task AddAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task UpdateAsync(Invoice invoice, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(long invoiceId, CancellationToken cancellationToken = default);
    Task<PaginationResult<Invoice>> GetAllAsync(BaseSpecification<Invoice> specification, CancellationToken cancellationToken = default);
}