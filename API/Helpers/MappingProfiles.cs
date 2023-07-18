using API.DTOs;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
            .ForMember(destination => destination.ProductBrand, source => source.MapFrom(s => s.ProductBrand.Name))
            .ForMember(destination => destination.ProductType, source => source.MapFrom(s => s.ProductType.Name))
            .ForMember(destination => destination.PictureUrl, source => source.MapFrom<ProductUrlResolver>());

            CreateMap<ProductBrand, ProductBrandToReturnDTO>();
            CreateMap<ProductType, ProductTypeToReturnDTO>();
            CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDTO>().ReverseMap();
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}