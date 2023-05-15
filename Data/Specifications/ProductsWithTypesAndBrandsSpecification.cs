using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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