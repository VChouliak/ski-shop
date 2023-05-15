using Core.Entities;
using Core.Interfaces.Data.Repository;

namespace Core.Interfaces.Service.Data
{
    public interface IBaseAsyncDataService : IAsyncDisposable
    {
         IBaseAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity;
         bool HasChanges();
         Task SaveAsync();
    }
}