using Application.Services;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBid.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/categorys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategorysAsync();
            return Ok(categories);
        }

        // GET: api/categorys/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        // POST: api/categorys
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory(CategoryDTO categoryDTO)
        {
            var category = await _categoryService.CreateCategoryAsync(categoryDTO);
            return CreatedAtAction(nameof(GetCategoryById), new { id = category.Id }, category);
        }

        // PUT: api/categorys/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryDTO categoryDTO)
        {
            var updated = await _categoryService.UpdateCategoryAsync(id, categoryDTO);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/categorys/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deleted = await _categoryService.DeleteCategoryAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
