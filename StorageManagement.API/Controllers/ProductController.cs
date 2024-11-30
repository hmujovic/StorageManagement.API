namespace StorageManagement.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetAll(cancellationToken);
            return Ok(response);
        }

        [HttpDelete("{productId}")]
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

        [HttpGet("details/{productId}")]
        public async Task<IActionResult> GetProductById(int productId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetById(productId, cancellationToken);
            return Ok(response);
        }

        [HttpGet("byCategory/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.GetByCategoryId(categoryId, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] ProductUpdateDto productDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.ProductService.Update(productId, productDto, cancellationToken);
            return Ok(response);
        }
    }
}