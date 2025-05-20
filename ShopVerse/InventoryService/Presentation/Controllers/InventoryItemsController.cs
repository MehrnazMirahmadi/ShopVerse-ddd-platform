using Application.Inventory.Commands.UpdateInventoryItem;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryItemsController(ISender sender) : ControllerBase
{
    /// <summary>
    /// دریافت لیست کالاهای انبار
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetInventoryItemsQuery(), cancellationToken);
        return Ok(result.InventoryItems);
    }

    /// <summary>
    /// افزودن کالای جدید به انبار
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CreateInventoryItemResult), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateInventoryItemCommand command,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = result.id }, result);
    }

    /// <summary>
    /// به‌روزرسانی کالای انبار
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(UpdateInventoryItemResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateInventoryItemCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.inventoryItem.Id)
        {
            return BadRequest("Mismatch between route ID and body ID");
        }

        var result = await sender.Send(command, cancellationToken);
        return Ok(result);
    }
}
