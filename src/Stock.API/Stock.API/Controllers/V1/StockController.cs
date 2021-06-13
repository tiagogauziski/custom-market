using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stock.Application.Stock.Command;

namespace Stock.API.Controllers.V1
{
    /// <summary>
    /// Stock controller actions.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockController"/> class.
        /// </summary>
        /// <param name="mediator">Command and query mediator.</param>
        public StockController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// Increase stock for given product.
        /// </summary>
        /// <param name="productId">Product id.</param>
        /// <param name="command">Increase product command.</param>
        /// <returns>HTTP response.</returns>
        [HttpPut]
        [Route("{productId}/increase")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Increase(
            [FromRoute]Guid productId,
            [FromBody]IncreaseStockCommand command)
        {
            command.ProductId = productId;

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors.Select(error => error.Message));
            }
        }
    }
}
