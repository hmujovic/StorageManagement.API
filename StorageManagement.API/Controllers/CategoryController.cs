namespace StorageManagement.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController(IServiceManager serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.GetAll(cancellationToken);
            return Ok(response);
        }

        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> Delete(int categoryId, CancellationToken cancellationToken)
        {
            await serviceManager.CategoryService.Delete(categoryId, cancellationToken);
            return NoContent();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDto categoryDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.Create(categoryDto, cancellationToken);
            return Ok(response);
        }

        [HttpGet("details/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.GetById(categoryId, cancellationToken);
            return Ok(response);
        }

        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateDto CategoryDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.Update(categoryId, CategoryDto, cancellationToken);
            return Ok(response);
        }
    }
}