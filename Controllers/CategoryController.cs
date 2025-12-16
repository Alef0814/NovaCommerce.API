using Microsoft.AspNetCore.Mvc;
using NovaCommerce.API.DTOs;
using NovaCommerce.API.Services;


namespace NovaCommerce.API.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            return result == null ? NotFound() : Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new{ id = result.Id},result);
        }
        


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted  = await _service.DeleteAsync(id);

            return deleted ? NoContent() : NotFound();
        }
    }
}