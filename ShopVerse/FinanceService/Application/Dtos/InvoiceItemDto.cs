namespace Finance.Application.Dtos;

public record InvoiceItemDto(long InvoiceId, string Name, decimal TotalAmount);

