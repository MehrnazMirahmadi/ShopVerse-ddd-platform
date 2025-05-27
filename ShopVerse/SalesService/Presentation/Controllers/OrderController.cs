namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(ISender sender) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
    {
        var result = await sender.Send(command);
        return Ok(result);
    }
}

