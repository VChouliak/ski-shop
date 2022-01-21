using API.DTO;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {


        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductsController(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repository.ListAsync(new ProductsWithTypesAndBrandsSpecification());
            if (products is not null)
            {
                return Ok(_mapper.Map<IReadOnlyList<Product>, List<ProductDTO>>(products));
            }
            return BadRequest("It was not possible to get a Productslist");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetEntityAsync(new ProductsWithTypesAndBrandsSpecification(id));
            if (product is not null)
            {
                return Ok(_mapper.Map<Product, ProductDTO>(product));
            }
            return NotFound("Product not found");
        }
    }
}