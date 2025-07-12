using Microsoft.Extensions.Logging;

namespace Finance.Application.Finance.Commands.CreateInvoice;

public class CreateInvoiceCommandHandler(IUnitOfWork unitOfWork,ILogger<CreateInvoiceCommand> _logger) : ICommandHandler<CreateInvoiceCommand, Result<long>>
{
    public async Task<Result<long>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        _logger.LogWarning("🔥 CreateInvoiceCommandHandler called for Invoice: {InvoiceNumber}", request.InvoiceNumber);

        var invoice = Invoice.Create(
            invoiceNumber: request.InvoiceNumber,
            issuedAt: request.IssuedAt,
            relatedEntityType: request.RelatedEntityType,
            relatedEntityId: request.RelatedEntityId
        );

        foreach (var item in request.Items)
        {
            invoice.AddItem(item.Description, item.UnitPrice, item.Quantity);
        }

        await unitOfWork.InvoiceRepository.AddAsync(invoice, cancellationToken);
        await unitOfWork.SaveChangesAsync();
        return Result<long>.Success(invoice.Id, "Invoice created successfully");


    }
}

