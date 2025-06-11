namespace Finance.Application.Finance.Commands.CreateInvoice;

public record CreateInvoiceCommand(
    long RelatedEntityId,
    string RelatedEntityType,
    string InvoiceNumber,
    DateTime IssuedAt,
    List<CreateInvoiceItemModel> Items
) : ICommand<Result<long>>;

public record CreateInvoiceItemModel(string Description, decimal UnitPrice, int Quantity);

