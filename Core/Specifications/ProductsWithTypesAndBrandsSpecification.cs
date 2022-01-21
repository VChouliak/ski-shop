using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(product=>product.ProductType);
            AddInclude(product=>product.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x=>x.Id == id) 
        {
            AddInclude(product=>product.ProductType);
            AddInclude(product=>product.ProductBrand);
        }
    }
}