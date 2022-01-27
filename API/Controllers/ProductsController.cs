using System.Net;
using API.DTO;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public ProductsController(IRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] ProductSpecificationParameters productParams)
        {
            var products = await _repository.ListAsync(new ProductsWithTypesAndBrandsSpecification(productParams));
            if (products is not null)
            {
                var totalItems = await _repository.CountAsync(new ProductWithFilterForCountSpecification(productParams));
                var productsDTO = _mapper.Map<IReadOnlyList<Product>, List<ProductDTO>>(products);
                var result = new Pagination<ProductDTO>(
                    productParams.PageIndex,
                    productParams.PageSize,
                    totalItems,
                    productsDTO
                    );

                return Ok(result);
            }
            else if (!products.Any())
            {
                return NotFound(new ApiResponse(404, "Productlist is empty, no data was found"));
            }
            return StatusCode((int)HttpStatusCode.InternalServerError, new ApiResponse(500));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repository.GetEntityAsync(new ProductsWithTypesAndBrandsSpecification(id));
            if (product is not null)
            {
                return Ok(_mapper.Map<Product, ProductDTO>(product));
            }
            return NotFound(new ApiResponse(404, "Product was not found"));
        }
    }
}