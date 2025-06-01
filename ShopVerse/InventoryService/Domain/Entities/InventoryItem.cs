namespace Domain.Entities;

public class InventoryItem : Aggregate<InventoryItemId>
{
    public string Name { get; private set; } = default!;
    public int Quantity { get; private set; }
    public ProductId ProductId { get; private set; } = default!;

    private InventoryItem() { }

    public InventoryItem(InventoryItemId id, string name, int quantity,ProductId productId)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
        ProductId = productId;
    }
    public void IncreaseQuantity(int quantity)
    {
        Quantity += quantity;
    }

}
