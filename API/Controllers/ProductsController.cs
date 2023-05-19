using API.DTOs;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Service.Data;
using Data.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IBaseAsyncDataService _dataService;
        private readonly IMapper _mapper;

        public ProductsController(IBaseAsyncDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]ProductSpecificationParameters productParams)
        {            
            var products = await _dataService.Repository<Product>().GetAllAsync(new ProductsWithTypesAndBrandsSpecification(productParams));

            if (products != null)
            {
                var totalItems = await _dataService.Repository<Product>().CountAsync(new ProductWithFiltersForCountSpecification(productParams));
                var data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDTO>>(products);
                
                return Ok(new Pagination<ProductToReturnDTO>(productParams.PageIndex, productParams.PageSize, totalItems, data));
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _dataService.Repository<Product>().GetEntityWithSpecificationAsync(new ProductsWithTypesAndBrandsSpecification(product => product.Id == id));

            if (product != null)
            {
                return Ok(_mapper.Map<Product, ProductToReturnDTO>(product));
            }
            return NotFound();
        }
    }
}