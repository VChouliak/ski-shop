using API.DTOs;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductsController(IBaseAsyncDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _dataService.Repository<Product>().GetAllAsync(new ProductsWithTypesAndBrandsSpecification());            
            
            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(products));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _dataService.Repository<Product>().GetEntityWithSpecificationAsync(new ProductsWithTypesAndBrandsSpecification(product => product.Id == id)) ;
           
            return Ok(_mapper.Map<Product,ProductToReturnDTO>(product));
        }
    }
}