using System.Collections;
using Core.Entities;
using Core.Interfaces.Data.Repository;
using Core.Interfaces.Service.Data;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Service.Data
{
    public class BaseAsyncDataService : IBaseAsyncDataService
    {
        private readonly DbContext _context;
        private Hashtable _repositories;

        public BaseAsyncDataService(DbContext context)
        {
            _context = context;
        }

        public virtual async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public virtual bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public virtual IBaseAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(BaseAsyncRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return _repositories[type] as IBaseAsyncRepository<TEntity>;
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}