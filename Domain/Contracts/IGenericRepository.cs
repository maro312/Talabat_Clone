namespace Domain.Contracts;

public interface IGenericRepository<TEntity,TKey> where TEntity : ModelBase<TKey>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(TKey id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}