using Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PizzaAPI.Models;

namespace PizzaAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            var result = await _mediator.Send(request.ToCommand());
            return Ok(result);
        }
    }
}
