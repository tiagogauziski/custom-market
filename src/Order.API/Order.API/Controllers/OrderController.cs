using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Order.Commands;

namespace Order.API.Controllers
{
    /// <summary>
    /// API endpoints for handling with orders.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Endpoint to create an order.
        /// </summary>
        /// <param name="createOrderCommand">Create order command.</param>
        /// <returns>HTTP response.</returns>
        [HttpPost]
        public Task<IActionResult> Create(CreateOrderCommand createOrderCommand)
        {
            return Task.FromResult(Ok() as IActionResult);
        }
    }
}
