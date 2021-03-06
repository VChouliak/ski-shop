using System.Linq.Expressions;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetEntityAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T>? specification);
        Task<int> CountAsync(ISpecification<T>? specification);
    }
}