using Catalog.Application.Dtos;
using Catalog.Application.Products.Commands.CreateProduct;
using Catalog.Application.Products.Commands.UpdateProduct;
using Catalog.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ISender _sender;

    public ProductController(ISender sender)
    {
        _sender = sender;
    }

    // POST api/product
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
    {
        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return BadRequest(result.ErrorMessage);

        return Ok(result);
    }

    // PUT api/product/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(string id, [FromBody] UpdateProductDto dto)
    {
        
        if (!ProductId.TryParse(id, out var productId))
        {
            return BadRequest("Invalid product ID format.");
        }

        var command = new UpdateProductCommand(productId, dto);

        var result = await _sender.Send(command);

        if (!result.IsSuccess)
            return NotFound(result.ErrorMessage);

        return Ok(result);
    }
}
