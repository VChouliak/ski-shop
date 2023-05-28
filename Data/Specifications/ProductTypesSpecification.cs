using System.Linq.Expressions;
using Core.Entities;

namespace Data.Specifications
{
    public class ProductTypesSpecification : BaseSpecification<ProductType>
    {
        public ProductTypesSpecification()
        {
        }

        public ProductTypesSpecification(int id) : this(x => x.Id == id)
        {
        }

        public ProductTypesSpecification(Expression<Func<ProductType, bool>> criteria) : base(criteria)
        {
        }
    }
}