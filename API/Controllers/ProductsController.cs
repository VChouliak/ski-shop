using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _context;

        public ProductsController(IProductRepository context)
        {            
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.GetProductsAsync();
            return products is not null ? Ok(products) : BadRequest("Some thing went wrong....");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _context.GetProductByIdAsync(id);
            return result is not null ? Ok(result) : NotFound($"Product was not found");
        }
    }
}