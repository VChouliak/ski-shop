using System.Linq.Expressions;
using Core.Entities;

namespace Data.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParameters productParams)
        : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        )
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
            AddOrderBy(product => product.Name);
            AddPagination(skip: productParams.PageSize * (productParams.PageIndex - 1), take: productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
        {
            AddInclude(product => product.ProductType);
            AddInclude(product => product.ProductBrand);
        }


        public string Sort { get; }
    }
}