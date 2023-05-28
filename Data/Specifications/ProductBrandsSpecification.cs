using System.Linq.Expressions;
using Core.Entities;

namespace Data.Specifications
{
    public class ProductBrandsSpecification : BaseSpecification<ProductBrand>
    {
        public ProductBrandsSpecification()
        {
        }

        public ProductBrandsSpecification(int id) : this (x=>x.Id == id)
        {
        }

        public ProductBrandsSpecification(Expression<Func<ProductBrand, bool>> criteria) : base(criteria)
        {
        }
    }
}