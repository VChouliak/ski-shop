using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Interfaces.Service.Data;
using Data.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TypesController : BaseApiController
    {
        private IBaseAsyncDataService _dataService;
        private IMapper _mapper;

        public TypesController(IBaseAsyncDataService dataService, IMapper mapper)
        {
            _dataService = dataService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            var brands = await _dataService.Repository<ProductType>().GetAllAsync();

            if (brands != null)
            {
                var data = _mapper.Map<IEnumerable<ProductType>, IEnumerable<ProductTypeToReturnDTO>>(brands);

                return Ok(data);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetProductType(int id)
        {
            var type = await _dataService.Repository<ProductType>().GetEntityWithSpecificationAsync(new ProductTypesSpecification(id));

            if (type != null)
            {               
                return Ok(_mapper.Map<ProductType, ProductTypeToReturnDTO>(type));
            }
            return NotFound();
        }
    }
}