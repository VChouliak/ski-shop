using API.DTOs;
using AutoMapper;
using Core.Entities;
using Settings;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        public ProductUrlResolver()
        {
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return Path.Combine(StoreSetting.Instance.ApiUrl, source.PictureUrl);
            }

            return null;
        }
    }
}