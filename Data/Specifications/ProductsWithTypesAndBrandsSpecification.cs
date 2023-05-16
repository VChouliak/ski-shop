using System.Linq.Expressions;
using Core.Entities;

namespace Data.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {       
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(product=>product.ProductType);
            AddInclude(product=>product.ProductBrand);
        }
        public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> criteria) : base(criteria)
        {
            AddInclude(product=>product.ProductType);
            AddInclude(product=>product.ProductBrand);
        }
    }
}