using Core.Entities;
using Core.Interfaces.Data.Specification;

namespace Core.Interfaces.Data.Repository
{
    public interface IBaseAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
        Task<TEntity> GetEntityWithSpecificationAsync(ISpecification<TEntity> specification);
        //TODO: Extend with other Methods later.
    }
}