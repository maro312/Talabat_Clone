using Domain;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GenericRepository<TEntity,TKey>(StoreDBContext context) : IGenericRepository<TEntity,TKey> where TEntity : ModelBase<TKey>
{
    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await context.Set<TEntity>().ToListAsync();

    public async Task<TEntity> GetByIdAsync(TKey id)
        => await context.Set<TEntity>().FindAsync(id);

    public void Add(TEntity entity)
        => context.Set<TEntity>().Add(entity);

    public void Update(TEntity entity)
        => context.Set<TEntity>().Update(entity);

    public void Delete(TEntity entity)
        => context.Set<TEntity>().Remove(entity);
}