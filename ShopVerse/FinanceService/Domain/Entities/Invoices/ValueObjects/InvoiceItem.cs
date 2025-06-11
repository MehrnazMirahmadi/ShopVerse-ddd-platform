namespace Finance.Domain.Entities.Invoices.ValueObjects;

public class InvoiceItem
{
    public string Description { get; private set; }
    public decimal UnitPrice { get; private set; }
    public int Quantity { get; private set; }

    public decimal TotalPrice => UnitPrice * Quantity;

    private InvoiceItem() { }

    public InvoiceItem(string description, decimal unitPrice, int quantity)
    {
        Description = description;
        UnitPrice = unitPrice;
        Quantity = quantity;
    }
}


