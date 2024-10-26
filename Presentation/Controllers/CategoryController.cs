using Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController(IServiceManager serviceManager) : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.GetAll(cancellationToken);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize]
        [HttpGet("details/{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId, CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.GetById(categoryId, cancellationToken);
            return Ok(response);
        }

        [Authorize]
        [HttpPut("update/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] CategoryUpdateDto CategoryDto, CancellationToken cancellationToken)
        {
            var response = await serviceManager.CategoryService.Update(categoryId, CategoryDto, cancellationToken);
            return Ok(response);
        }
    }
}
