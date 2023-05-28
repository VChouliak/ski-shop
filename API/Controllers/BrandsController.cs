using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Service.Data;
using Data.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BrandsController : BaseApiController
    {
        private IBaseAsyncDataService _dataService;
        private IMapper _mapper;

        public BrandsController(IBaseAsyncDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _dataService.Repository<ProductBrand>().GetAllAsync();

            if (brands != null)
            {
                var data = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<ProductBrandToReturnDTO>>(brands);

                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductBrand(int id)
        {
            var brand = await _dataService.Repository<ProductBrand>().GetEntityWithSpecificationAsync(new ProductBrandsSpecification(id));

            if (brand != null)
            {               
                return Ok(_mapper.Map<ProductBrand, ProductBrandToReturnDTO>(brand));
            }
            return NotFound();
        }
    }
}