using Domain.Models.Products;

namespace Domain.Contracts;

public interface IUnitOfWork
{
    // IGenericRepository<Product,int> ProductRepo { get; }

    IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : ModelBase<TKey>;
    
    Task<int> SaveChangesAsync();
}