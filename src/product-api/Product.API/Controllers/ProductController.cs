using System;
using System.Net.Mime;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Command.Product.Commands;
using Product.Application.Command.Result;
using Product.Application.Query.Product.Queries;
using Product.Application.Query.Product.Responses;

namespace Product.API.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [ProducesResponseType(typeof(ProductResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery(id)).ConfigureAwait(false);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Uri), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccessful)
            {
                if (result is ConflictResult<Guid>)
                {
                    return base.Conflict(result.Errors);
                }
                else
                {
                    return base.BadRequest(result.Errors);
                }
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result.Data);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Uri), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string[]), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(UpdateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccessful)
            {
                if (result is ConflictResult<Guid>)
                {
                    return base.Conflict(result.Errors);
                }
                else if (result is NotFoundResult<Guid>)
                {
                    return base.NotFound();
                }
                else
                {
                    return base.BadRequest(result.Errors);
                }
            }

            return CreatedAtAction(nameof(GetById), new { id = result.Data }, result.Data);
        }
    }
}
