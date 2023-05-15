using Core.Entities;
using Core.Interfaces.Service.Data;
using Data.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IBaseAsyncDataService _dataService;

        public ProductsController(IBaseAsyncDataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dataService.Repository<Product>().GetAllAsync(new ProductsWithTypesAndBrandsSpecification());            
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _dataService.Repository<Product>().GetEntityWithSpecification(new ProductsWithTypesAndBrandsSpecification(product => product.Id == id)) ;
            return Ok(product);
        }
    }
}