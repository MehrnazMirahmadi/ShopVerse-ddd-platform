using Application.Sales.Queries.GetOrders;
using MediatR;
using ShopVerse.BuildingBlocks.Paging;
using System.Net.NetworkInformation;

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
    [HttpGet]
    public async Task<IActionResult> GetOrders(
              [FromQuery] PaginationRequest paging,
              [FromQuery] string? filterName = null,
              [FromQuery] bool sortByQuantityDesc = false,
              CancellationToken cancellationToken = default)
    {
        var query = new GetOrdersQuery(paging, filterName, sortByQuantityDesc);

        var result = await sender.Send(query);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result.Value);
    }
}

