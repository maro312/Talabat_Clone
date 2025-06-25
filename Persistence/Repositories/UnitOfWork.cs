using Domain;
using Domain.Contracts;

namespace Persistence.Repositories;

public class UnitOfWork(StoreDBContext context) : IUnitOfWork
{
    private readonly Dictionary<string, object> _repositories = new Dictionary<string,object>();
    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : ModelBase<TKey>
    {
           var TypeName = typeof(TEntity).Name;
           if (_repositories.ContainsKey(TypeName))
           {
               return (IGenericRepository<TEntity, TKey>)_repositories[TypeName];
           }

           var repo = new GenericRepository<TEntity, TKey>(context);
           _repositories.Add(TypeName, repo);
           return repo;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}