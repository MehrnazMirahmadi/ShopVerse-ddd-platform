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
}
