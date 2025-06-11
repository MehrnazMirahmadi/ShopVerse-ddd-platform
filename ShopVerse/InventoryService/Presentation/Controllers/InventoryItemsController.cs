using Application.Inventory.Commands.UpdateInventoryItem;
using Application.Inventory.Queries.CheckInvetoryAvailability;
using Application.Inventory.Queries.GetInventoryItems;
using ShopVerse.BuildingBlocks.Paging;

[ApiController]
[Route("api/[controller]")]
public class InventoryItemsController(ISender sender) : ControllerBase
{
    
    [HttpGet]
    public async Task<IActionResult> GetAll(
    [FromQuery] PaginationRequest paging,
    CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetInventoryItemsQuery(paging),
            cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return CreatedAtAction(nameof(GetAll), new { id = result.Value.id }, result.Value);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateInventoryItemCommand command, CancellationToken cancellationToken)
    {
        if (id != command.inventoryItem.Id)
            return BadRequest("Mismatch between route ID and body ID");

        var result = await sender.Send(command, cancellationToken);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(result.Value);
    }
    [HttpGet("check-availability/{productId}/{quantity}")]
    public async Task<IActionResult> CheckAvailability(Guid productId, int quantity)
    {
        var query = new CheckInventoryAvailabilityQuery(productId, quantity);
        var result = await sender.Send(query);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(new { isAvailable = result.Value });
    }

}
