using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces.Data.Specification
{
    public interface ISpecification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity, object>>> Includes { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; }
        public Expression<Func<TEntity, object>> GroupBy { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
}