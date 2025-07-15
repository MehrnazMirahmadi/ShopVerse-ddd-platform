using Application.Sales.Commands.CreateProduct;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result);
    }
}

