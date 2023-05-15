using Core.Entities;
using Core.Interfaces.Data.Specification;

namespace Core.Interfaces.Data.Repository
{
    public interface IBaseAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetEntityWithSpecification(ISpecification<TEntity> specification);
        //TODO: Etend with other Methods later.
    }
}