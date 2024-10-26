using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetAll(cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{accountId}")]
        public async Task<IActionResult> Delete(int productId, CancellationToken cancellationToken)
        {
            await serviceManager.ProductService.Delete(productId, cancellationToken);
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.Create(productDto, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("details/{productId}")]
        public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetById(productId, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpGet("byCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetByCategoryId(categoryId, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDto productDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.Update(productId, productDto, cancellationToken);
            return Ok(response);
        }
    }
}
