using Core.Entities;
using Core.Interfaces.Data.Repository;
using Core.Interfaces.Data.Specification;
using Data.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseAsyncRepository<TEntity> : IBaseAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext _context;
        public BaseAsyncRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task<TEntity> GetEntityWithSpecification(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), specification);
        }
    }
}