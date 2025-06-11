using Finance.Domain.Entities.Invoices.ValueObjects;
using Finance.Domain.Entities.Payments;
using Finance.Domain.Enums;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Finance.Domain.Entities.Invoices;

public class Invoice : Aggregate<long>
{
    private readonly List<InvoiceItem> _items = new();
    private readonly List<Payment> _payments = new();

    public string InvoiceNumber { get; private set; }
    public DateTime IssuedAt { get; private set; }
    public decimal TotalAmount => _items.Sum(i => i.TotalPrice);
    public InvoiceStatus Status { get; private set; }

    public long RelatedEntityId { get; private set; }
    public string RelatedEntityType { get; private set; }

    public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();
    public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

    // برای EF Core
    private Invoice() { }

    // ✅ فقط داخل متد Create از این استفاده می‌کنیم
    private Invoice(string invoiceNumber, DateTime issuedAt, string relatedEntityType, long relatedEntityId)
    {
        InvoiceNumber = invoiceNumber;
        IssuedAt = issuedAt;
        RelatedEntityType = relatedEntityType;
        RelatedEntityId = relatedEntityId;
        Status = InvoiceStatus.Draft;
        CreatedAt = DateTime.UtcNow;
    }

    // ✅ Factory Method پیشنهادی
    public static Invoice Create(string invoiceNumber, DateTime issuedAt, string relatedEntityType, long relatedEntityId)
    {
        if (string.IsNullOrWhiteSpace(invoiceNumber))
            throw new ArgumentException("Invoice number is required.", nameof(invoiceNumber));

        if (string.IsNullOrWhiteSpace(relatedEntityType))
            throw new ArgumentException("Related entity type is required.", nameof(relatedEntityType));

        if (relatedEntityId <= 0)
            throw new ArgumentException("Related entity ID must be greater than zero.", nameof(relatedEntityId));

        return new Invoice(invoiceNumber, issuedAt, relatedEntityType, relatedEntityId);
    }

    public void AddItem(string description, decimal unitPrice, int quantity)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required.", nameof(description));

        if (unitPrice <= 0)
            throw new ArgumentException("Unit price must be greater than zero.", nameof(unitPrice));

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));

        var item = new InvoiceItem(description, unitPrice, quantity);
        _items.Add(item);
    }

    public void AddPayment(Payment payment)
    {
        if (payment == null)
            throw new ArgumentNullException(nameof(payment));

        _payments.Add(payment);
        UpdateStatusBasedOnPayments();
    }

    private void UpdateStatusBasedOnPayments()
    {
        var totalPaid = _payments
            .Where(p => p.Status == PaymentStatus.Completed)
            .Sum(p => p.Amount);

        if (totalPaid == 0)
            Status = InvoiceStatus.Unpaid;
        else if (totalPaid < TotalAmount)
            Status = InvoiceStatus.PartiallyPaid;
        else
            Status = InvoiceStatus.Paid;
    }
}
