using Finance.Application.Finance.Queries.GetAllInvoices;

namespace Finance.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController
        : ControllerBase
    {
        private readonly ISender _sender;

        public InvoiceController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(
                    [FromQuery] PaginationRequest paging,
                    CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetInvoicesQuery(paging), cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result.Value);
        }
    }
}
