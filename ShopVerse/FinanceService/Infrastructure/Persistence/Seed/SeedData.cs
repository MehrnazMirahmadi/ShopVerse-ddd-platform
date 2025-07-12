namespace Finance.Infrastructure.Persistence.Seed;

public static class SeedData
{
    public static void Initialize(FinanceDbContext context)
    {
        if (!context.Invoices.Any())
        {
            var invoice = Invoice.Create(
                invoiceNumber: "INV-001",
                issuedAt: DateTime.UtcNow,
                relatedEntityType: "Order",
                relatedEntityId: Guid.NewGuid() 
            );

            invoice.AddItem(
                description: "محصول اولیه",
                unitPrice: 50000,
                quantity: 1
            );

            context.Invoices.Add(invoice);
            context.SaveChanges();
        }
    }
}
