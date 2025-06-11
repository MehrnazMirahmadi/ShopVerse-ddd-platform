using Finance.Domain.Enums;
using ShopVerse.BuildingBlocks.Abstractions;

namespace Finance.Domain.Entities.Payments;

public class Payment : Aggregate<Guid>
{
    public string ReferenceCode { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime PaidAt { get; private set; }
    public PaymentMethod Method { get; private set; }
    public PaymentStatus Status { get; private set; }

    public long InvoiceId { get; private set; }
    public string PayerType { get; private set; }
    public long PayerId { get; private set; }

    private Payment() { }

    public Payment(decimal amount, string referenceCode, DateTime paidAt,
                   PaymentMethod method, long invoiceId, string payerType, long payerId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        ReferenceCode = referenceCode;
        PaidAt = paidAt;
        Method = method;
        Status = PaymentStatus.Completed; // فرض بر اینه که پرداخت موفق بوده
        InvoiceId = invoiceId;
        PayerType = payerType;
        PayerId = payerId;

        CreatedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
        Status = PaymentStatus.Failed;
        LastModified = DateTime.UtcNow;
    }
}