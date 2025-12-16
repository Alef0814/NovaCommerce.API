using Microsoft.AspNetCore.Mvc; 
using NovaCommerce.API.DTOs;
using NovaCommerce.API.Services.Interfaces;



namespace NovaCommerce.API.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
    
        
        public ProductController(IProductService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
    
            return result == null ? NotFound() : Ok(result); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductDTO dto)
        {
            var result = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = result.Id}, result);

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDTO dto)
        {
            var updatedProduct = await _service.UpdateAsync(id, dto);
    
            return updatedProduct is null ? NotFound() : NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var delete = await _service.DeleteAsync(id);
        
            return delete ? NoContent() : NotFound();
        }
    }
}