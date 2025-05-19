namespace Domain.Entities;

public class InventoryItem : Aggregate<InventoryItemId>
{
    public string Name { get; private set; } = default!;
    public int Quantity { get; private set; }

    private InventoryItem() { }

    public InventoryItem(InventoryItemId id, string name, int quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }


}
