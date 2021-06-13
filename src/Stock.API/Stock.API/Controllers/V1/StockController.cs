using System;
using System.Threading.Tasks;
using FluentResults;
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
        /// <summary>
        /// Initializes a new instance of the <see cref="StockController"/> class.
        /// </summary>
        public StockController()
        {
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
        public Task<IActionResult> Increase(
            [FromRoute]Guid productId,
            [FromBody]IncreaseStockCommand command)
        {
            return Task.FromResult(Ok() as IActionResult);
        }
    }
}
